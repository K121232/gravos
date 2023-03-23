using UnityEngine;

public class EQGrappleAP : MonoBehaviour {
    public  Transform   targetPoint;

    public  Vector2 GetPoint () {
        return targetPoint.localPosition;
    }
}
