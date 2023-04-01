using UnityEngine;

public class SmoothTracker : MonoBehaviour {
    public  Transform   target;
    public  Rigidbody2D trackingRigidbody;

    public  Vector3     offset;
    public  float       velocityFactor;

    public  float       strength;
    public  float       rotationStrength;

    private Vector3     delta;

    public virtual void Start () {
        if ( trackingRigidbody == null ) {
            trackingRigidbody = target.GetComponent<Rigidbody2D> ();
        }
    }

    public virtual void LateUpdate() {
        if ( trackingRigidbody != null ) {
            delta = ( Vector3 )target.position + ( Vector3 )trackingRigidbody.velocity * velocityFactor;
        } else {
            delta = target.position;
        }

        delta += offset;

        transform.position = Vector3.Lerp( transform.position - offset, delta, strength ) + offset;
        transform.rotation = Quaternion.Lerp ( transform.rotation, target.rotation, rotationStrength * Time.unscaledDeltaTime );
    }
}
