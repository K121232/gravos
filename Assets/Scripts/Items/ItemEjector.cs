using UnityEngine;

public class ItemEjector : Turret {
    public  ItemPort[]      ports;
    private int             lastItem = -1;

    public override void Start () {
        base.Start ();
        Eject ( 0 );
    }

    public void Eject ( int target ) {
        lastItem = target;
        TriggerHold ();
    }

    public override void TriggerRelease () {
        base.TriggerRelease ();
        lastItem = -1;
    }

    public override GameObject Fire ( Vector2 prv ) {
        if ( lastItem == -1 || ports [ lastItem ].item == null ) return null;
        transform.rotation = Quaternion.Euler ( 0, 0, Random.Range ( -180, 180 ) );
        GameObject delta = base.Fire ( prv );
        ports [ lastItem ].item.Attach ( delta.GetComponent<ItemPort> () );
        TriggerRelease ();
        return delta;
    }
}
