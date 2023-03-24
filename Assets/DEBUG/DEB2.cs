using UnityEngine;

public class DEB2 : MonoBehaviour {
    public Transform    target;

    public  float       radius;
    public  float       radialSpeed;

    private void Start () {
        target.position = Vector2.up * radius;
    }

    void FixedUpdate () {
        target.position = Quaternion.Euler ( 0, 0, radialSpeed * Time.fixedDeltaTime ) * target.position.normalized * radius;
        target.Rotate ( 0, 0, radialSpeed * Time.fixedDeltaTime );
    }
}
