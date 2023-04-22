using UnityEngine;

public class ItemEjector : Turret {
    public  ItemPort[]      ports;
    private int             lastItem = -1;

    public override void Start () {
        base.Start ();
    }

    public void Eject ( int target ) {
        if ( ports [ target ] != null && ports [ target ].item != null ) {
            lastItem = target;
            Fire ();
        }
    }

    public override GameObject Fire () {
        if ( lastItem == -1 || ports [ lastItem ].item == null ) return null;
        GameObject delta = base.Fire ();
        if ( delta == null ) return null;
        ports [ lastItem ].item.Attach ( delta.GetComponent<ItemPort> () );
        lastItem = -1;
        return delta;
    }

    private void OnDisable () {
        for ( int i = 0; i < ports.Length; i++ ) {
            Eject ( i );
        }
    }
}
