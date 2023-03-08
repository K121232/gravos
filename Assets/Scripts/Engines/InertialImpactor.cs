using UnityEngine;

public class InertialImpactor : MonoBehaviour {
    public  float   velocity;

    private Vector3 v0;

    public void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = v0 + transform.up * velocity;
        v0 = Vector3.zero;
    }

    public void Prime ( Rigidbody2D rgb ) {
        if ( rgb != null ) {
            v0 = rgb.velocity;
        }
    }
}
