using UnityEngine;

public class AimErrorPlotter : DataPlotter {
    public  TFC     tfc;

    public override void Start () {        
        lineHigh = tfc.coneMaxDeviation;
        lineLow = -lineHigh;

        base.Start ();
    }

    public override void LateUpdate () {
        if ( tfc.controller.target != null ) {
            sample = tfc.controller.GetAIMDeviation();
        } else {
            sample = 0;
        }
        base.LateUpdate ();
    }
}
