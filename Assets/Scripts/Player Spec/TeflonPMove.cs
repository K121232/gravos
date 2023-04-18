using UnityEngine;

public class TeflonPMove : Movement {
    public  Konig         sod;

    public  bool        armourLock = false;
    public override void Start () {
        armourLock  = false;
        innerHeading = transform.up;
        base.Start ();
    }

    public  bool        flybywire;
    public  Camera      cam;

    public  Vector2     innerHeading;
    public  float       trimSTR = 1;

    public  Vector2     DEBUGSAVE;

    public override void Update() {
        if ( flybywire ) {
            Vector2 targetDir = cam.ScreenToWorldPoint ( Input.mousePosition ) - transform.position;
            if ( Input.GetAxis ( "Vertical" ) >= 0 ) {
                delta += targetDir.normalized * Mathf.Max ( Input.GetAxis ( "Vertical" ), 0 );
            } else {
                if ( rgb.velocity.magnitude > 2.5f ) {
                    delta += Mathf.Max ( Vector2.Dot ( -rgb.velocity.normalized, transform.up ), 0 ) * -rgb.velocity.normalized;
                }
            }
            /*
            deltaAngle += FilterAngle ( sod.NextFrame ( Time.fixedDeltaTime,
                Vector2.SignedAngle ( transform.up, targetDir ),
                Vector2.SignedAngle ( Vector2.up, targetDir ), rgb.angularVelocity ) ) / Time.fixedDeltaTime;
            */
        } else {
            Vector2 deltaI = new Vector2 ( Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical") );
            Debug.DrawLine ( DEBUGSAVE * 10, deltaI * 10, Color.green, 1000 );
            DEBUGSAVE = deltaI;
            if ( deltaI.magnitude > 0.01f ) {
                innerHeading = deltaI.normalized;
            }
            innerHeading = Quaternion.Euler ( 0, 0, Input.GetAxis ( "TrimRotation" ) * trimSTR ) * innerHeading;

            /*
            deltaAngle += FilterAngle ( sod.NextFrame ( Time.fixedDeltaTime,
                Vector2.SignedAngle ( transform.up, innerHeading ),
                Vector2.SignedAngle ( Vector2.up, innerHeading ), rgb.angularVelocity ) ) / Time.fixedDeltaTime;
            */
           
            delta += innerHeading * deltaI.magnitude;
        }
        transform.rotation = Quaternion.FromToRotation ( Vector2.up, rgb.velocity );
        base.Update();
    }

    public override void FixedUpdate() {
        if ( armourLock ) {
            delta       = Vector2.zero;
        }
        base.FixedUpdate();
    }

    public  void SetLock ( bool a ) {
        armourLock = a;
    }
}
