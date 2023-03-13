using UnityEngine;

public class AimHelper : MonoBehaviour {
    private CameraTether    tether;
    private Camera          cam;
    
    public  Rigidbody2D     bodyA;
    public  Rigidbody2D     bodyB;

    public  float           STRPD;      // Position delta
    public  float           STRORS;     // Ortographic size

    private Vector3         originalOffset;
    private float           originalSize;

    public  float           maxDistance;

    public  float           minOrtoSize;
    public  float           maxOrtoSize;

    private void Start () {
        cam = GetComponent<Camera> ();
        tether = GetComponent<CameraTether> ();
        originalOffset = tether.offset;
        originalSize = cam.orthographicSize;
    }

    void LateUpdate () {
        Vector2 delta = ( bodyB.transform.position - bodyA.transform.position );
        Vector2 filter =  new Vector2 ( ((float)Screen.height)/((float)Screen.width), 1 );

        delta.Scale ( filter );
        if ( delta.magnitude > maxDistance ) { delta = delta.normalized * maxDistance; }
        filter.x = 1.0f / filter.x;
        filter.y = 1.0f / filter.y;
        delta.Scale ( filter );

        tether.offset = originalOffset + (Vector3)delta * STRPD;
        cam.orthographicSize = Mathf.Clamp ( originalSize + ( bodyB.transform.position - bodyA.transform.position ).magnitude * STRORS, minOrtoSize, maxOrtoSize );
    }
}
