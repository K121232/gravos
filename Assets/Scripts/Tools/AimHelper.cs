using UnityEngine;

public class AimHelper : MonoBehaviour {
    private SmoothTracker    tether;
    private Camera          cam;
    
    public  Transform       bodyA;
    public  Transform       bodyB;

    public  float           STRD;       // Damping

    public  float           STRPD;      // Position delta
    public  float           STRORS;     // Ortographic size

    private Vector3         originalOffset;
    private float           originalSize;

    public  float           maxDistance;

    public  float           minOrtoSize;
    public  float           maxOrtoSize;

    public  float           userFactor = 1;

    private bool            lockout;

    private void Start () {
        cam = GetComponent<Camera> ();
        tether = GetComponent<SmoothTracker> ();
        originalOffset = tether.offset;
        originalSize = cam.orthographicSize;
        lockout = true;
        LockIn ( bodyB );
    }

    public void LockIn ( Transform a ) {
        bodyB = a;
        lockout = bodyA == null || bodyB == null;
    } 

    void LateUpdate () {
        Vector2 delta   = Vector2.zero;
        float   deltaS  = 0;

        userFactor -= Input.GetAxis ( "Mouse ScrollWheel" );
        userFactor = Mathf.Clamp ( userFactor, 0.1f, 2 );

        if ( !lockout ) {
            deltaS = ( bodyB.position - bodyA.position ).magnitude * STRORS;

            delta = ( bodyB.position - bodyA.position ) * STRPD * userFactor;
            Vector2 filter =  new Vector2 ( ((float)Screen.height)/((float)Screen.width), 1 );

            delta.Scale ( filter );
            if ( delta.magnitude > maxDistance ) { delta = delta.normalized * maxDistance; }
            delta.x /= filter.x;
            delta.y /= filter.y;
        }

        tether.offset = Vector3.Lerp ( tether.offset, originalOffset + (Vector3)delta, STRD * Time.smoothDeltaTime );
        // Delta Low / High Ortho Size
        float dLOS = Mathf.Clamp ( minOrtoSize * userFactor, minOrtoSize, maxOrtoSize );
        float dHOS = Mathf.Clamp ( maxOrtoSize * userFactor, minOrtoSize, maxOrtoSize );
        cam.orthographicSize = Mathf.Lerp ( cam.orthographicSize, Mathf.Clamp ( originalSize + deltaS, dLOS, dHOS ), STRD * Time.smoothDeltaTime );
    }
}
