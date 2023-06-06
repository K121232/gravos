using UnityEngine;

public class TeflonPMove : TeflonMovement {
    public  Konig         sod;

    public  bool        armourLock = false;
    public override void Start () {
        armourLock = false;
        innerHeading = transform.up;
        base.Start ();
    }

    public  bool        flybywire;
    public  Camera      cam;

    public  Vector2     innerHeading;
    public  float       trimSTR = 1;

    public override void Update () {
        if ( flybywire ) {
            if ( Input.GetAxis ( "Vertical" ) >= 0 ) {
                innerHeading = cam.ScreenToWorldPoint ( Input.mousePosition ) - transform.position;
                delta += Mathf.Max ( Input.GetAxis ( "Vertical" ), 0 );
            } else {
                if ( rgb.velocity.magnitude > 2.5f ) {
                    innerHeading = -rgb.velocity;
                    delta += Mathf.Max ( Vector2.Dot ( -rgb.velocity.normalized, transform.up ), 0 );
                }
            }
        } else {
            Vector2 deltaI = new Vector2 ( Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical") );
            if ( deltaI.magnitude > 0.25f ) {
                innerHeading = deltaI.normalized;
            }
            delta += Mathf.Max ( Vector2.Dot ( transform.up, deltaI ), 0 );
        }

        deltaAngle += FilterAngle ( sod.NextFrame ( 0, 
            Vector2.SignedAngle ( Quaternion.Euler ( 0, 0, Input.GetAxis ( "TrimRotation" ) * trimSTR ) * innerHeading, transform.up ), 
            Time.fixedDeltaTime ) 
            ) / Time.fixedDeltaTime;

        base.Update ();
    }

    public override void FixedUpdate () {
        if ( armourLock ) {
            delta = 0;
            deltaAngle = 0;
            angleDrag = 0;
        }
        base.FixedUpdate ();
    }

    public void SetLock ( bool a ) {
        armourLock = a;
    }
}