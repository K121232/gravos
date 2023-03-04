using UnityEngine;

public class InertialImpactor : MonoBehaviour {
    public  float   velocity;
    public  float   drag;

    private Vector3 v0;

    public void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = v0 + transform.up * velocity;
        v0 = Vector3.zero;
        Debug.DrawLine ( transform.position, transform.position + ( Vector3 ) GetComponent<Rigidbody2D> ().velocity / 10, Color.red );
    }

    public void Prime ( Rigidbody2D rgb ) {
        if ( rgb != null ) {
            v0 = rgb.velocity;
        }
    }
}
