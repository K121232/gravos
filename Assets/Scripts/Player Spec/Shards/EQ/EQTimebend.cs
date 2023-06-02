using UnityEngine;

public class EQTimebend : UnifiedOrdnance {
    [Header("EQ Time Bend")]
    public float bendSTR;
    private float pastScale = 1;

    public override void OnStartFire () {
        if ( controller == null ) return;
        pastScale = Time.timeScale;
    }

    public override void OnStopFire () {
        if ( controller == null ) return;
        Time.timeScale = pastScale;
    }

    public override void Fire () {
        if ( controller == null ) return;
        if ( bendSTR != Time.timeScale ) {
            pastScale = Time.timeScale;
            Time.timeScale = bendSTR;
        }
    }
}
