using UnityEngine;

public class AimHelper : MonoBehaviour {
    private CameraTether    tether;
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

    private bool            lockout;

    private void Start () {
        cam = GetComponent<Camera> ();
        tether = GetComponent<CameraTether> ();
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

        if ( !lockout ) {

            deltaS = ( bodyB.position - bodyA.position ).magnitude * STRORS;

            delta = ( bodyB.position - bodyA.position ) * STRPD;
            Vector2 filter =  new Vector2 ( ((float)Screen.height)/((float)Screen.width), 1 );

            delta.Scale ( filter );
            if ( delta.magnitude > maxDistance ) { delta = delta.normalized * maxDistance; }
            delta.x /= filter.x;
            delta.y /= filter.y;
        }

        tether.offset = Vector3.Lerp ( tether.offset, originalOffset + (Vector3)delta, STRD * Time.deltaTime );
        cam.orthographicSize = Mathf.Lerp ( cam.orthographicSize, Mathf.Clamp ( originalSize + deltaS, minOrtoSize, maxOrtoSize ), STRD * Time.deltaTime );
    }
}
