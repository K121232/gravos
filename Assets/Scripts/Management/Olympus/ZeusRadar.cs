using UnityEngine;

public class ZeusRadar : Zeus {
    public  Radar           radar;
    private Collider2D      delta;

    protected override void Start () {
        delta = null;
        base.Start ();
    }

    public override void SetFiringLock ( bool alpha ) {
        base.SetFiringLock ( alpha );
        delta = null;
    }

    void Update () {
        if ( delta != radar.GetClosestElement ( delta ) ) {
            delta = radar.GetClosestElement ( delta );
            ModifyTarget ( delta ? delta.transform : null );
        }
    }
}
