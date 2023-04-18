using UnityEngine;

public class PCFCM : FCM {
    // POWER CELL
    public  PowerCell   cell;
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

    public override GameObject Fire () {
        cell.VariDrain ( ( isRefiring ? drainRate : drainInitial ) * Time.unscaledDeltaTime );
        isRefiring = true;
        return base.Fire ();
    }
}
