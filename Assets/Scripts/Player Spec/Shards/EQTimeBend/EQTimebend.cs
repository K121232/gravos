using UnityEngine;

public class EQTimebend : EQBase {
    [Header ( "EQ Time Bend" )]
    public  float       bendSTR;
    private float       pastScale;

    public override void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        base.MainInit ( port );
        if ( enabled ) {
            cell = port.batteryLink.GetChild ( 1 ).GetComponent<PowerCell> ();
        } else {
            cell = null;
        }
    }

    public override void Update () {
        base.Update ();
    }

    public override void TriggerHold () {
        if ( !triggerDown ) {
            pastScale = Time.timeScale;
        }
        base.TriggerHold ();
    }

    public override void TriggerRelease () {
        if ( triggerDown ) {
            Time.timeScale = pastScale;
        }
        base.TriggerRelease ();
    }

    public override GameObject Fire () {
        if ( bendSTR != Time.timeScale ) {
            pastScale = Time.timeScale;
            Time.timeScale = bendSTR;
        }
        return base.Fire ();
    }
}
