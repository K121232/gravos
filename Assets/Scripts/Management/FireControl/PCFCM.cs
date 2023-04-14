using UnityEngine;

public class PCFCM : FCM {
    // POWER CELL
    public  PowerCell   cell;
    public  float       shotCost;
    public  float       initialCost;

    private bool        isRefiring;

    public override void Reload () {
        isRefiring = false;
    }

    public override bool AmmoCheck () {
        return cell.GetAvailableLoad () >= ( isRefiring ? shotCost : initialCost );
    }

    public override void TriggerRelease () {
        base.TriggerRelease ();
        isRefiring = false;
    }

    public override GameObject Fire () {
        cell.VariDrain ( isRefiring ? shotCost : initialCost );
        isRefiring = true;
        return base.Fire ();
    }
}
