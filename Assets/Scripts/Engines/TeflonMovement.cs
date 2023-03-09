using UnityEngine;

public class TeflonMovement : MonoBehaviour {
    protected Rigidbody2D rgb;

    public  float           acc;
    public  float           mxv;

    public  float           angleAcc;
    public  float           angleMxv;

    public    float           angleDrag;
    protected float           delta;
    protected float           deltaAngle;
    protected float               backupDelta;
    protected float               backupDeltaAngle;
    protected int                 ticks;

    public void UpdateDeltas ( float alphaAcc, float alphaAngle, bool clipAngle = true ) {
        if ( clipAngle ) {
            deltaAngle += FilterAngle ( deltaAngle );
        } else {
            deltaAngle += alphaAngle;
        }
        delta += Mathf.Clamp ( alphaAcc, -1, 1 );
    }

    public void DEBUGANG ( float a, Color b ) {
        Vector3 offset = new Vector2( 5, 5 );
        Debug.DrawLine ( offset + Quaternion.Euler ( 0, 0, a ) * Vector2.up, offset, b );
    }

    public static float VClip( float a, float b, float m ) {
        if ( Mathf.Abs( b + a ) >= m ) {
            if ( Mathf.Sign( a ) != Mathf.Sign( b ) ) { return Mathf.Sign( a ) * Mathf.Min( m, Mathf.Abs( a ) ); }
            if ( Mathf.Abs( b ) > m ) { b = Mathf.Sign( b ) * m; }
            return Mathf.Sign( a ) * ( m - Mathf.Abs( b ) );
        }
        return a;
    }

    Vector2 Cutter ( Vector2 a, Vector2 b, float m ) {
        if ( a.magnitude < 0.0001f ) return Vector2.zero;
        if ( ( a + b ).magnitude >= m ) {
            if ( Vector2.Dot( a, b ) < 0 ) { return a.normalized * Mathf.Min ( a.magnitude, m ); }
            if ( b.magnitude > m ) { b = b.normalized * m; }
            return ( b + a ).normalized * m - b;
        }
        return a;
    }

    public void AddAngle ( float a ) {
        if ( angleAcc == 0 ) return;
        deltaAngle += FilterAngle ( a );
    }
    
    public float FilterAngle ( float a ) {
        if ( angleAcc == 0 ) return 0;
        return Mathf.Clamp ( a, -angleAcc, angleAcc ) / angleAcc;
    }

    virtual public void Start() {
        rgb = GetComponent<Rigidbody2D>();
    }

    virtual public void Update() {
        ticks++;
        Debug.DrawLine ( transform.position + new Vector3 ( 3, 0 ), transform.position + new Vector3 ( 3, 0 ) + Quaternion.Euler ( 0, 0, rgb.angularVelocity ) * Vector2.up, Color.cyan );
    }

    virtual public void FixedUpdate() {
        if ( ticks == 0 ) { delta = backupDelta; deltaAngle = backupDeltaAngle; } else { deltaAngle /= ticks; delta /= ticks; backupDeltaAngle = deltaAngle; backupDelta = delta; }

        deltaAngle = VClip( deltaAngle * angleAcc * Time.fixedDeltaTime, rgb.angularVelocity, angleMxv );

        rgb.angularVelocity += deltaAngle;
        rgb.angularVelocity -= angleDrag * rgb.angularVelocity * Time.fixedDeltaTime;

        Vector2 deltaProcessed = Cutter( transform.up * delta * acc, rgb.velocity, mxv );

        //deltaProcessed = Vector3.Project( deltaProcessed, transform.up );
        //Debug.Log( rgb.velocity.magnitude.ToString( "0.00" ) );
        //Debug.Log( Vector2.Dot( deltaProcessed.normalized, transform.up ).ToString("0.00") );

        //Debug.Log ( deltaBB.magnitude.ToString("0.00") + " " + rgb.velocity.magnitude.ToString ("0.00") + " " + deltaProcessed.magnitude.ToString ( "0.00" ) );

        rgb.AddForce( deltaProcessed * rgb.mass * Time.fixedDeltaTime, ForceMode2D.Impulse );

        deltaAngle = 0;
        delta = 0;
        ticks = 0;
    }
}
