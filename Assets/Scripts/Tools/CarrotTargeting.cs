using UnityEngine;

public class CarrotTargeting : MonoBehaviour {
    public  Camera          cam;
    public  Transform       carrot;

    private void OnDisable () {
        if ( carrot != null ) {
            carrot.localPosition = Vector3.zero;
        }
    }

    void LateUpdate () {
        Vector2 delta = cam.ScreenToWorldPoint ( Input.mousePosition );
        carrot.position = delta;
        carrot.rotation = transform.rotation;
    }   
}
