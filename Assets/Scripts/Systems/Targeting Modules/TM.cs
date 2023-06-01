using UnityEngine;

public class TM : MonoBehaviour {
    protected TeflonMovement      PROPULSION;
    protected Rigidbody2D         rgb;

    public  Konig         sod;

    public  bool        variThrust;

    protected   bool        thrustBypass;

    public virtual void Start () {
        PROPULSION = GetComponent<TeflonMovement> ();
        rgb = GetComponent<Rigidbody2D> ();
        thrustBypass = false;
    }

    protected   Vector2 targetLink;
    protected   float   thurstLink;

    public virtual void Update () {
        Debug.DrawLine ( transform.position, targetLink, Color.yellow );

        targetLink -= (Vector2)transform.position;
        /*
        Debug.DrawLine ( transform.position, transform.position + (Vector3)deltaH, Color.green );
        Debug.DrawLine ( transform.position, target.position, Color.red );
        Debug.DrawLine ( transform.position, transform.position + transform.up * 4, Color.blue );
        */
        float deltaAA = sod.NextFrame ( 0, Vector2.SignedAngle ( targetLink, transform.up ), Time.deltaTime );
        if ( !thrustBypass ) {
            if ( variThrust ) {
                thurstLink = Mathf.Max ( Vector2.Dot ( transform.up, targetLink.normalized ), 0 );
            } else {
                thurstLink = 1;
            }
        }
        thrustBypass = false;

        PROPULSION.UpdateDeltas ( thurstLink, PROPULSION.FilterAngle ( deltaAA ) / Time.deltaTime, false );
    }
}
