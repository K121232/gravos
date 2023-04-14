using UnityEngine;

public class IMP : MonoBehaviour {
    public  float   velocity;

    private Vector3 v0;

    public void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = v0 + transform.up * velocity;
        v0 = Vector3.zero;
    }

    public void Prime ( Vector2 _v0 ) {
        v0 = _v0;
    }
}
