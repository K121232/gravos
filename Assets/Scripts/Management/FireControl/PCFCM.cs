using UnityEngine;

public class PCFCM : FCM {
    // POWER CELL FIRE CONTROL MECHANISM
    public  int         cellSelection = -1;

    protected PowerCell cell;
    public  float       drainRate = 1;            // drain per second
    public  float       drainInitial = 0;

    private bool        isRefiring;

    public override void Reload () {
        isRefiring = false;
    }

    public override bool AmmoCheck () {
        return cell.GetLoad () != 0;
    }

    public override void TriggerRelease () {
        base.TriggerRelease ();
        isRefiring = false;
    }

    public override void Fire () {
        cell.VariDrain ( ( isRefiring ? drainRate : drainInitial ) * Time.unscaledDeltaTime );
        isRefiring = true;
        base.Fire ();
    }

    public  void LoadCell ( Transform cellLink ) {
        if ( cellSelection > -1 && cellSelection < cellLink.childCount ) {
            cell = cellLink.GetChild ( cellSelection ).GetComponent<PowerCell> ();
        }
    }
}
