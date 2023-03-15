using UnityEngine;

public class CarrotTargeting : MonoBehaviour {
    public  Camera          cam;
    public  Transform       carrot;

    public  Transform       carrotStew;

    void LateUpdate () {
        Vector2 delta = cam.ScreenToWorldPoint ( Input.mousePosition );
        if ( carrotStew != null ) delta = carrotStew.position;
        carrot.position = delta;
        carrot.rotation = transform.rotation;
    }
}
