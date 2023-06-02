using UnityEngine;

public class CoreFireable : UnifiedOrdnance, UnifiedOrdnanceFireInterface {
    public      PoolSpooler payloadLoader;
    public      PoolSpooler trailLoader;

    public override void OnStartFire () {}

    public override void OnStopFire () { }

    protected GameObject transfer;

    public override void Fire () {}
}
