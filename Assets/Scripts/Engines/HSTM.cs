using UnityEngine;

public class HSTM : MonoBehaviour {
    // Oh its Heat seeking targeting module ( dumb name )
    private TeflonMovement      PROPULSION;
    private Rigidbody2D         rgb;

    public  Transform   target;
    public  Rigidbody2D targetRGB;

    public  float       STRV1;
    public  float       STRV2;

    private SOD         sod;

    public  float       f;
    public  float       z;
    public  float       r;

    public  bool        regen;

    public void Start () {
        regen = true;
        PROPULSION      = GetComponent<TeflonMovement> ();
        rgb             = GetComponent<Rigidbody2D> ();
    }

    private void OnEnable () {
        regen = true;
        if ( target != null && rgb == null ) {
            target.TryGetComponent<Rigidbody2D> ( out targetRGB );
        }
    }

    private void OnDisable () {
        rgb     = null;
        target  = null;
    }

    public void Bind ( Transform alpha ) {
        target = alpha;
        alpha.TryGetComponent ( out targetRGB );
    }

    public void Update () {
        Vector2 deltaH = target.position - transform.position + ( Vector3 ) rgb.velocity * STRV1;

        if ( targetRGB != null ) deltaH += targetRGB.velocity * STRV2;

        if ( regen ) {
            sod = new SOD ( f, z, r, Vector2.SignedAngle ( Vector2.up, deltaH ) );
            regen = false;
        }

        float deltaAA;

        deltaAA = sod.Update ( Time.fixedDeltaTime, Vector2.SignedAngle ( transform.up, deltaH ), Vector2.SignedAngle ( Vector2.up, deltaH ), rgb.angularVelocity );

        /*
        Debug.DrawLine ( transform.position, transform.position + (Vector3)deltaH );
        Debug.DrawLine ( transform.position, target.position, Color.red );
        Debug.DrawLine ( transform.position, transform.position + transform.up * 4, Color.blue );
        */

        PROPULSION.UpdateDeltas ( 1, deltaAA, false );
    }
}
