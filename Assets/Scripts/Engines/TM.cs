using UnityEngine;

public class TM : MonoBehaviour {
    // Oh its Heat seeking targeting module ( dumb name )
    protected TeflonMovement      PROPULSION;
    protected Rigidbody2D         rgb;

    protected SOD         sod;

    public  float       f;
    public  float       z;
    public  float       r;

    public  bool        regen;

    public virtual void Start () {
        regen = true;
        PROPULSION = GetComponent<TeflonMovement> ();
        rgb = GetComponent<Rigidbody2D> ();
    }

    public  Vector2 targetLink;
    public virtual void Update () {
        targetLink -= (Vector2)transform.position;
        // Disable this when there is no debug
        if ( regen ) {
            sod = new SOD ( f, z, r, Vector2.SignedAngle ( Vector2.up, targetLink ) );
            regen = false;
        }
        /*
        Debug.DrawLine ( transform.position, transform.position + (Vector3)deltaH, Color.green );
        Debug.DrawLine ( transform.position, target.position, Color.red );
        Debug.DrawLine ( transform.position, transform.position + transform.up * 4, Color.blue );
        */
        float deltaAA = sod.Update ( Time.deltaTime, Vector2.SignedAngle ( transform.up, targetLink ), Vector2.SignedAngle ( Vector2.up, targetLink ), rgb.angularVelocity );

        PROPULSION.UpdateDeltas ( 1, PROPULSION.FilterAngle ( deltaAA ) / Time.deltaTime, false );
    }
}
