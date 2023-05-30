using UnityEngine;

public class AimErrorPlotter : DataPlotter {
    public  Thunder thunder;

    public override void Start () {        
        lineHigh = 10;
        lineLow = -lineHigh;

        base.Start ();
    }

    public override void LateUpdate () {
        if ( thunder.target != null ) {
            sample = thunder.GetAIMDeviation();
        } else {
            sample = 0;
        }
        base.LateUpdate ();
    }
}
