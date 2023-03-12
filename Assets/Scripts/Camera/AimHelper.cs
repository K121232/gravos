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

    public  float           minOrtoSize;
    public  float           maxOrtoSize;

    private void Start () {
        cam = GetComponent<Camera> ();
        tether = GetComponent<CameraTether> ();
        originalOffset = tether.offset;
        originalSize = cam.orthographicSize;
    }

    void LateUpdate () {
        tether.offset = originalOffset + ( bodyB.transform.position - bodyA.transform.position ) * STRPD;
        cam.orthographicSize = Mathf.Clamp ( originalSize + ( bodyB.transform.position - bodyA.transform.position ).magnitude * STRORS, minOrtoSize, maxOrtoSize );
    }
}
