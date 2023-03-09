using UnityEngine;

public class TeflonPMove : TeflonMovement {
    public float angleControlDrag;
    public float angleNeutralDrag;
    protected float angleDragBackup;

    private SOD         sod;

    public  float       f;
    public  float       z;
    public  float       r;

    public  bool        regen;
    public  bool        armourLock = false;

    public override void Start () {
        regen = true;
        armourLock = false;
        base.Start ();
    }

    public  bool        flybywire;
    public  Camera      cam;

    public override void Update() {

        if ( regen ) {
            sod = new SOD ( f, z, r, Vector2.SignedAngle ( Vector2.up, -rgb.velocity ) );
            regen = false;
        }

        if ( flybywire ) {
            deltaAngle += FilterAngle ( sod.Update ( Time.fixedDeltaTime,
                Vector2.SignedAngle ( transform.up, cam.ScreenToWorldPoint ( Input.mousePosition ) - transform.position ),
                Vector2.SignedAngle ( Vector2.up, cam.ScreenToWorldPoint ( Input.mousePosition ) - transform.position ), rgb.angularVelocity ) ) / Time.fixedDeltaTime;
        }

        if ( Input.GetAxis( "Vertical" ) < 0 && rgb.velocity.magnitude > 5 ) {
            angleDrag   += angleNeutralDrag;
            deltaAngle  += FilterAngle ( sod.Update ( Time.fixedDeltaTime, 
                Vector2.SignedAngle ( transform.up, -rgb.velocity ), 
                Vector2.SignedAngle ( Vector2.up, -rgb.velocity ), rgb.angularVelocity ) ) / Time.fixedDeltaTime;
            delta       += Vector2.Dot( transform.up, -rgb.velocity.normalized ) > 0.99f ? 1 : 0;
        } else {
            if ( !flybywire ) {
                deltaAngle -= Input.GetAxis ( "Horizontal" );
            }
            delta       += Mathf.Max( Input.GetAxis( "Vertical" ), 0 );
            
        }

        if ( Mathf.Abs ( deltaAngle ) > 0.25f ) {
            angleDrag += angleControlDrag;
        } else {
            angleDrag += angleNeutralDrag;
        }
        // WARNING, angle neutral drag has no autoupdate at rotation indicator 
        base.Update();
    }

    public override void FixedUpdate() {
        if ( ticks != 0 ) { angleDrag /= ticks; angleDragBackup = angleDrag; } else { angleDrag = angleDragBackup; }
        if ( armourLock ) {
            delta       = 0;
            deltaAngle  = 0;
            angleDrag = 0;
        }
        base.FixedUpdate();
        angleDrag = 0;
    }

    public  void SetLock ( bool a ) {
        armourLock = a;
    }
}
