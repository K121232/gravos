using UnityEngine;

public class TeleportLeash : MonoBehaviour {
    public  Transform   anchor;
    private Rigidbody2D anchorRgb;
    private Rigidbody2D rgb;

    public  float       leashRange;

    public  float       respawnMaxSpeed;
    public  float       respawnMinRange;

    private void Start () {
        anchor.TryGetComponent ( out anchorRgb );
        TryGetComponent ( out rgb );
    }

    private void Update () {
        if ( ( rgb.transform.position - anchor.position ).magnitude > leashRange ) {
            rgb.transform.position = anchor.position + anchor.up * -respawnMinRange;
            if ( anchorRgb != null ) {
                rgb.velocity = Vector3.Project ( rgb.velocity, anchorRgb.velocity );
                rgb.velocity += anchorRgb.velocity;
            }
            if ( rgb.velocity.magnitude > respawnMaxSpeed ) {
                rgb.velocity = rgb.velocity.normalized * respawnMaxSpeed;
            }
            // What to do when the respawn happens
        }
    }
}
