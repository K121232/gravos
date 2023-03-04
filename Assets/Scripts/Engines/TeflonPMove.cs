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

    public override void Start () {
        regen = true;
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

    /*  Debug speedometer 
    private float[] DEBUG1 = new float[ 200 ];
    private int     DEBUG2;
    public  float   DEBUGS;
    public  float   DEBUGSY;
    public  Vector2 DEBUGOFF;

    private void LateUpdate () {
        for ( int i = 0; i < 199; i++ ) {
            Debug.DrawLine ( (Vector2) transform.position + DEBUGOFF + new Vector2 ( i * DEBUGS, DEBUG1 [ i ] ), ( Vector2 )transform.position + DEBUGOFF + new Vector2 ( ( i + 1 ) * DEBUGS, DEBUG1 [ i + 1 ] ) );
        }
        Debug.DrawLine ( ( Vector2 )transform.position + DEBUGOFF + new Vector2 ( 0, DEBUGSY ), ( Vector2 )transform.position + DEBUGOFF + new Vector2 ( 200 * DEBUGS, DEBUGSY ), Color.red );
    }
    */

    public override void FixedUpdate() {
        if ( ticks != 0 ) { angleDrag /= ticks; angleDragBackup = angleDrag; } else { angleDrag = angleDragBackup; }
        /*
        DEBUG1 [ DEBUG2++ ] = ( rgb.velocity.magnitude / mxv ) * DEBUGSY;
        DEBUG2 %= 200;
        */
        base.FixedUpdate();
        angleDrag = 0;
    }
}
