using UnityEngine;

public class SmoothTracker : MonoBehaviour {
    public  Transform   target;
    public  Rigidbody2D trackingRigidbody;

    public  Vector3     offset;
    public  float       velocitySTR;

    public  float       rotationLerpSTR;
    public  float       translationLerpSTR;

    private Vector3     delta;
    public  bool        scaledTime = false;

    public virtual void Start () {
        if ( trackingRigidbody == null ) {
            trackingRigidbody = target.GetComponent<Rigidbody2D> ();
        }
    }

    public virtual void LateUpdate () {
        if ( trackingRigidbody != null ) {
            delta = target.position + ( Vector3 ) trackingRigidbody.velocity * velocitySTR;
        } else {
            delta = target.position;
        }

        delta += offset;

        float timescalefactor = scaledTime ? Time.deltaTime : Time.unscaledDeltaTime ;
        transform.position = Vector3.Lerp ( transform.position - offset, delta, translationLerpSTR * timescalefactor ) + offset;
        transform.rotation = Quaternion.Lerp ( transform.rotation, target.rotation, rotationLerpSTR * timescalefactor );
    }
}
