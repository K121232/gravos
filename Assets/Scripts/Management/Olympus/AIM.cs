using UnityEngine;
using UnityEngine.Rendering;

public class AIM : MonoBehaviour, ThunderMinion {
    protected   Thunder     controller;
    protected   Rigidbody2D targetRGB;

    public  enum        TargetingMethod { SIMPLE, PREDICT };
    public TargetingMethod  targetingMethod = TargetingMethod.SIMPLE;
    [Tooltip("Multiparameter : \n" +
        " SIMPLE:\t strength of velocity prediction \n" +
        " PREDICT:\t velocity of the projectile")]
    public  float           multiParam1 = 1;

    public      float       traversalSpeed;
    protected   Konig       konig;

    protected float       pastDeviation;
    protected Quaternion  pastOrientation;

    public void SetController ( Thunder thunder ) {
        controller = thunder;
    }

    public void Start () {
        konig = GetComponent<Konig> ();
        pastOrientation = transform.rotation;
    }

    public void ResetPD () {
        pastDeviation = Mathf.Infinity;
    }

    public Vector2 GetTGVSimple () {
        Vector3 omega = Vector3.zero;
        omega += ( Vector3 ) controller.GetVTarget ();
        omega -= ( Vector3 ) controller.GetV ();
        omega *= multiParam1;
        return omega + controller.target.position - transform.position;
    }

    public Vector2 GetTGVPredict () {
        Vector3 deltaV = ( Vector3 ) ( controller.GetVTarget () - controller.GetV () );
        Vector3 projectileV = ( controller.target.position - transform.position ).normalized * multiParam1;
        Vector3 impact = LineIntersection.GetIntersectionPoint ( 
            transform.position, 
            projectileV + deltaV, 
            controller.target.position, 
            deltaV );

        /*
        Debug.DrawLine ( transform.position, transform.position + projectileV, Color.red );
        Debug.DrawLine ( transform.position, transform.position + deltaV, Color.white );
        Debug.DrawLine ( transform.position, transform.position + projectileV + deltaV, Color.yellow );
        */

        return impact - transform.position;
    }

    private void Update () {
        Vector3 tgv = transform.parent.up;
        if ( controller.target != null ) {
            if ( targetingMethod == TargetingMethod.SIMPLE ) {
                tgv = GetTGVSimple ();
            }
            if ( targetingMethod == TargetingMethod.PREDICT ) {
                tgv = GetTGVPredict ();
            }
        }
        float delta = Vector2.SignedAngle ( transform.up, tgv );
        pastDeviation = delta;

        if ( traversalSpeed != 0 ) {
            float deltaANGV = controller.GetAngV( Quaternion.Angle ( pastOrientation, transform.rotation ) / Time.deltaTime );

            Debug.Log ( pastDeviation + " !!!! " + deltaANGV );

            delta = konig.NextFrame ( Time.deltaTime, delta, Vector2.SignedAngle ( Vector2.up, tgv ), deltaANGV );
            delta = Mathf.Clamp ( delta, -traversalSpeed, traversalSpeed ) * Time.deltaTime;

            pastOrientation = transform.rotation;
            transform.Rotate ( Vector3.forward, delta * Time.timeScale );
        }

        Debug.DrawLine ( transform.position, transform.position + transform.up * 30, Color.green );
        Debug.DrawLine ( transform.position, transform.position + tgv, Color.red );
    }

    public  float   GetLastDeviation () {
        return pastDeviation;
    }

}
