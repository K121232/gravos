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

    public  float           maxOrtoSize;
    public  float           maxDist;

    private void Start () {
        cam = GetComponent<Camera> ();
        tether = GetComponent<CameraTether> ();
        originalOffset = tether.offset;
        originalSize = cam.orthographicSize;
    }

    void LateUpdate () {
        tether.offset = ( bodyB.transform.position - bodyA.transform.position ) * STRPD;
        tether.offset += originalOffset;
        //if ( tether.offset.sqrMagnitude > maxDist * maxDist ) { tether.offset = tether.offset.normalized * maxDist; }
        cam.orthographicSize = Mathf.Min ( originalSize + ( bodyB.transform.position - bodyA.transform.position ).magnitude * STRORS, maxOrtoSize );
    }
}
