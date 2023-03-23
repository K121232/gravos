using UnityEngine;

public class TeleportLeash : MonoBehaviour {
    public  Transform   anchor;
    private Rigidbody2D rgb;

    public  float       leashRange;

    public  float       respawnMaxSpeed;
    public  float       respawnMinRange;

    private void Start () {
        TryGetComponent ( out rgb );
    }

    private void Update () {
        if ( ( rgb.transform.position - anchor.position ).magnitude > leashRange ) {
            rgb.transform.position = anchor.position + ( rgb.transform.position - anchor.position ).normalized * respawnMinRange;
            if ( rgb.velocity.magnitude > respawnMaxSpeed ) {
                rgb.velocity = rgb.velocity.normalized * respawnMaxSpeed;
            }
            // What to do when the respawn happens
        }
    }
}
