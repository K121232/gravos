using UnityEngine;

public class STAnchor : SmoothTracker {
    public  Transform   headingTarget;
    public  float       radius;
    public override void LateUpdate () {
        offset = ( headingTarget.position - target.position ).normalized * radius;
        base.LateUpdate ();
    }
}
