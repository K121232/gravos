using UnityEngine;

public class TeflonPMove : TeflonMovement {
    private SOD         sod;

    public  float       f;
    public  float       z;
    public  float       r;

    public  bool        regen;
    public  bool        armourLock = false;

    public override void Start () {
        regen       = true;
        armourLock  = false;
        base.Start ();
        //sod.PassThrough ( 0.15f, 0.005f, 0.075f );
    }

    public  bool        flybywire;
    public  Camera      cam;

    public override void Update() {
        if ( regen ) {
            sod = new SOD ( f, z, r, Vector2.SignedAngle ( Vector2.up, -rgb.velocity ) );
            //Debug.Log ( sod.Outcore () );
            regen = false;
        }

        if ( flybywire ) {
            Vector2 targetDir = cam.ScreenToWorldPoint ( Input.mousePosition ) - transform.position;
            if ( Input.GetAxis ( "Vertical" ) >= 0 ) {
                delta += Mathf.Max ( Input.GetAxis ( "Vertical" ), 0 );
            } else {
                if ( rgb.velocity.magnitude > 2.5f ) {
                    targetDir = -rgb.velocity;
                    delta += Mathf.Max ( Vector2.Dot ( -rgb.velocity.normalized, transform.up ), 0 );
                }
            }
            deltaAngle += FilterAngle ( sod.Update ( Time.fixedDeltaTime,
                Vector2.SignedAngle ( transform.up, targetDir ),
                Vector2.SignedAngle ( Vector2.up, targetDir ), rgb.angularVelocity ) ) / Time.fixedDeltaTime;
        } else {
            Vector2 deltaI = new Vector2 ( Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical") );
            if ( deltaI.magnitude >= 0.1f ) {
                deltaAngle += FilterAngle ( sod.Update ( Time.fixedDeltaTime,
                    Vector2.SignedAngle ( transform.up, deltaI ),
                    Vector2.SignedAngle ( Vector2.up, deltaI ), rgb.angularVelocity ) ) / Time.fixedDeltaTime;
            }
            deltaAngle += Input.GetAxis ( "TrimRotation" );

            delta += Mathf.Max ( Vector2.Dot ( transform.up, deltaI ), 0 );
        }
        base.Update();
    }

    public override void FixedUpdate() {
        if ( armourLock ) {
            delta       = 0;
            deltaAngle  = 0;
            angleDrag   = 0;
        }
        base.FixedUpdate();
    }

    public  void SetLock ( bool a ) {
        armourLock = a;
    }
}
