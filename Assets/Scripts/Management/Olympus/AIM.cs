using UnityEngine;

public class AIM : MonoBehaviour {
    public      Thunder     controller;
    protected   Rigidbody2D targetRGB;

    public  float       traversalSpeed;
    public  float       velocityPredictStrength;
    private Konig       konig;

    private float       pastDeviation;

    public void Start () {
        konig = GetComponent<Konig> ();
    }

    private void Update () {
        Vector3 tgv = Vector3.zero;

        if ( controller.target == null ) {
            tgv = transform.parent.up;
        } else {
            tgv += ( Vector3 ) controller.GetVTarget ();
            tgv -= ( Vector3 ) controller.GetV ();

            tgv *= velocityPredictStrength;

            tgv += controller.target.position - transform.position;
        }

        float delta = Vector2.SignedAngle ( transform.up, tgv );
        pastDeviation = delta;

        if ( traversalSpeed != 0 ) {
            delta = konig.NextFrame ( Time.deltaTime, delta, Vector2.SignedAngle ( Vector2.up, tgv ), controller.GetAngV() );
            delta = Mathf.Clamp ( delta, -traversalSpeed, traversalSpeed ) * Time.deltaTime;
            transform.Rotate ( Vector3.forward, delta );
        }

        Debug.DrawLine ( transform.position, transform.position + transform.up * 5, Color.green );
        Debug.DrawLine ( transform.position, transform.position + tgv, Color.red );
    }

    public  float   GetLastDeviation () {
        return pastDeviation;
    }

}
