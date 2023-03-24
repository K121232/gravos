using UnityEngine;

public class DEB2 : MonoBehaviour {
    public  Transform       target;
    public  Transform       target2;

    public  float       radius;
    public  float       radialSpeed;

    private void Start () {
        target.position = Vector2.up * radius;
        if ( target2 != null ) {
            GetComponent<ForceTether> ().BaseInit ( target.GetComponent<Rigidbody2D> (), target );
            GetComponent<ForceTether> ().LoadBL ( target2.GetComponent<Rigidbody2D> (), target2, radius );
            GetComponent<ForceTether> ().enabled = true;
        }
    }

    void FixedUpdate () {
        target.position = target.parent.position + Quaternion.Euler ( 0, 0, radialSpeed * Time.fixedDeltaTime ) * ( target.position - transform.parent.position ).normalized * radius;
        target.Rotate ( 0, 0, radialSpeed * Time.fixedDeltaTime );
    }
}
