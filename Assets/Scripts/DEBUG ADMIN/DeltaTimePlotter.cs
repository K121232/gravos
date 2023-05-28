using UnityEngine;

public class DeltaTimePlotter : DataPlotter {
    
    public override void LateUpdate () {
        sample = Time.deltaTime;
        base.LateUpdate ();
    }
}
