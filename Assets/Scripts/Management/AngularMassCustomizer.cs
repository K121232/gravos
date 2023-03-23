using UnityEngine;

public class AngularMassCustomizer : MonoBehaviour {
    public  float       targetInertia;
    public  Transform   paramCenterOfMass;
    private void Start () {
        GetComponent<Rigidbody2D> ().inertia = targetInertia;
        if ( paramCenterOfMass != null ) {
            GetComponent<Rigidbody2D> ().centerOfMass = transform.worldToLocalMatrix * paramCenterOfMass.position;
        }
    }
}
