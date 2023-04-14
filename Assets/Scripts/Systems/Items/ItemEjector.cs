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
        fcm.TriggerHold ();
    }

    public override GameObject Fire () {
        if ( lastItem == -1 || ports [ lastItem ].item == null ) return null;
        transform.rotation = Quaternion.Euler ( 0, 0, Random.Range ( -180, 180 ) );
        GameObject delta = base.Fire ();
        ports [ lastItem ].item.Attach ( delta.GetComponent<ItemPort> () );
        lastItem = -1;
        return delta;
    }
}
