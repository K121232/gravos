using UnityEngine;

public class DeltaTimePlotter : DataPlotter {
    

    public override void LateUpdate () {
        Time.timeScale = 0.25f;
        sample = Time.deltaTime;
        base.LateUpdate ();
    }
}
