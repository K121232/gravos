using UnityEngine;

public class ItemEjector : Turret {
    public  ItemPort[]      ports;
    private int             lastItem = -1;

    public override void Start () {
        base.Start ();
    }

    public void Eject ( int target ) {
        if ( ports [ target ] != null && ports [ target ].GetItem () != null ) {
            lastItem = target;
            Fire ();
        }
    }

    public override GameObject Fire () {
        if ( lastItem == -1 || ports [ lastItem ].GetItem () == null ) return null;
        GameObject delta = base.Fire ();
        if ( delta == null ) return null;
        ports [ lastItem ].GetItem ().Autobind ( delta.GetComponent<ItemPort> () );
        lastItem = -1;
        return delta;
    }

    private void OnDisable () {
        for ( int i = 0; i < ports.Length; i++ ) {
            Eject ( i );
        }
    }
}
