using UnityEngine;

public class Movement : MonoBehaviour {
    protected Rigidbody2D rgb;

    public  float           acc;
    public  float           mxv;

    protected Vector2       delta;
    protected Vector2       backupDelta;
    protected int           ticks;

    public  bool            hasParticles = false;
    public  float           particleSTR = 0;
    private ParticleSystem  ps;

    private bool           vLock;
    Vector2 Cutter ( Vector2 a, Vector2 b, float m ) {
        if ( a.magnitude < 0.0001f ) return Vector2.zero;
        if ( ( a + b ).magnitude >= m ) {
            if ( Vector2.Dot ( a, b ) < 0 ) { return a.normalized * Mathf.Min ( a.magnitude, m ); }
            if ( b.magnitude > m ) { b = b.normalized * m; }
            return ( b + a ).normalized * m - b;
        }
        return a;
    }

    virtual public void AddV ( Vector2 a ) {
        delta += a;
    }

    virtual public void SetV ( Vector2 a ) {
        backupDelta = a;
        vLock = true;
    }

    virtual public void SetV ( float a ) {
        SetV ( transform.up * a );
    }

    virtual public void Start () {
        rgb = GetComponent<Rigidbody2D> ();
        ps = GetComponent<ParticleSystem> ();
    }

    virtual public void Update () {
        ticks++;
    }

    virtual public void FixedUpdate () {
        if ( ticks == 0 || vLock ) { delta = backupDelta; } else { delta /= ticks; backupDelta = delta; }
        vLock = false;

        Vector2 deltaProcessed = Cutter( delta * acc * Time.fixedDeltaTime, rgb.velocity, mxv );

        if ( hasParticles && deltaProcessed.magnitude > 1 ) {
            ps.Emit ( (int) ( particleSTR * deltaProcessed.magnitude ) );
        }

        rgb.AddForce ( deltaProcessed * rgb.mass / Time.fixedDeltaTime );

        delta = Vector2.zero;
        ticks = 0;
    }
}
