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

    public void SetController ( Thunder thunder ) {
        controller = thunder;
    }

    public void Start () {
        konig = GetComponent<Konig> ();
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
        float delta = Vector2.SignedAngle ( tgv, transform.up );
        pastDeviation = delta;

        if ( traversalSpeed != 0 ) {
            delta = konig.NextFrame ( 0, delta, Time.deltaTime );
            delta = Mathf.Clamp ( delta, -traversalSpeed, traversalSpeed ) * Time.deltaTime;

            transform.Rotate ( Vector3.forward, delta * Time.timeScale );
        }

        Debug.DrawLine ( transform.position, transform.position + transform.up * 30, Color.green );
        Debug.DrawLine ( transform.position, transform.position + tgv, Color.red );
    }

    public  float   GetLastDeviation () {
        return pastDeviation;
    }

}
