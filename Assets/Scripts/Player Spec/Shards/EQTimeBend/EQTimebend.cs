using UnityEngine;

public class EQTimebend : EQBase {
    [Header("EQ Time Bend")]
    public float bendSTR;
    private float pastScale;

    public override void Update () {
        base.Update ();
    }

    public override void OnStartFire () {
        pastScale = Time.timeScale;
    }

    public override void OnStopFire () {
        Time.timeScale = pastScale;
    }

    public override void Fire () {
        if ( bendSTR != Time.timeScale ) {
            pastScale = Time.timeScale;
            Time.timeScale = bendSTR;
        }
        base.Fire ();
    }
}
