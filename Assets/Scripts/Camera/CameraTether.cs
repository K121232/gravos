using UnityEngine;

public class CameraTether : MonoBehaviour {
    public  Transform   target;
    public  Vector3     offset;

    public  Rigidbody2D trackingRigidbody;
    public  float       velocityFactor;

    public  float       strength;
    public  float       rotationStrength;
    public  float       maxLength;

    private Vector3     delta;

    void LateUpdate() {
        if ( trackingRigidbody != null ) {
            delta = ( Vector3 )target.position + ( Vector3 )trackingRigidbody.velocity * velocityFactor;
        } else {
            delta = target.position;
        }
        
        delta += offset;

        // Implement max length here

        delta.z = offset.z;

        transform.position = Vector3.Lerp( transform.position - offset, delta, strength ) + offset;
        transform.rotation = Quaternion.Lerp ( transform.rotation, target.rotation, rotationStrength * Time.unscaledDeltaTime );
    }
}
