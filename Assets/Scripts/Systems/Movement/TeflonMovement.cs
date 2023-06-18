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

    private bool           vLock;
    public void UpdateDeltas ( float alphaAcc, float alphaAngle, bool clipAngle = true ) {
        if ( !enabled ) { alphaAcc = 0; alphaAngle = 0; }
        if ( clipAngle ) {
            deltaAngle += FilterAngle ( deltaAngle );
        } else {
            deltaAngle += alphaAngle;
        }
        delta += Mathf.Clamp ( alphaAcc, 0, 1 );
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
        if ( !enabled ) { a = 0; }
        deltaAngle += FilterAngle ( a );
    }
    
    public float FilterAngle ( float a ) {
        if ( angleAcc == 0 ) return 0;
        return Mathf.Clamp ( a, -angleAcc, angleAcc ) / angleAcc;
    }

    virtual public void AddThrusterOutput ( float a ) {
        delta += a;
    }

    virtual public void SetThrusterOutput ( float a ) {
        backupDelta = a;
        vLock       = true;
    }

    virtual public void Start() {
        rgb = GetComponent<Rigidbody2D>();
    }

    virtual public void Update() {
        ticks++;
    }

    virtual public void FixedUpdate() {
        if ( ticks == 0 ) { deltaAngle = backupDeltaAngle; } else { deltaAngle /= ticks; backupDeltaAngle = deltaAngle; }
        if ( ticks == 0 || vLock ) { delta = backupDelta; } else { delta /= ticks; backupDelta = delta; }
        vLock = false;

        deltaAngle = VClip( deltaAngle * angleAcc * Time.fixedDeltaTime, rgb.angularVelocity, angleMxv );

        if ( float.IsNaN ( deltaAngle ) ) deltaAngle = 0;
        rgb.angularVelocity += deltaAngle;
        rgb.angularVelocity -= angleDrag * rgb.angularVelocity * Time.fixedDeltaTime;

        Vector2 deltaProcessed = Cutter( transform.up * delta * acc * Time.fixedDeltaTime, rgb.velocity, mxv );

        rgb.AddForce( deltaProcessed * rgb.mass / Time.fixedDeltaTime );

        ResetDeltas ();
    }

    protected virtual void ResetDeltas () {
        deltaAngle = 0;
        delta = 0;
        ticks = 0;
    }

    protected virtual void OnEnable () {
        ResetDeltas ();
    }
}
