using UnityEngine;

public class AimErrorPlotter : DataPlotter {
    public  TargetingRig    rig;

    public override void Start () {        
        lineHigh = rig.coneMaxDeviation;
        lineLow = -lineHigh;

        base.Start ();
    }

    public override void LateUpdate () {
        if ( rig.target != null ) {
            sample = rig.pastDeviation;
        } else {
            sample = 0;
        }
        base.LateUpdate ();
    }
}
