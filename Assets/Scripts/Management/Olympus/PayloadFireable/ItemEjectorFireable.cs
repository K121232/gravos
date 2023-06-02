using UnityEngine;

public class ItemEjectorFireable : PayloadFireable {
    public  ItemPort[]      ports;
    private int             lastItem = -1;

    public void Eject ( int target ) {
        if ( ports [ target ] != null && ports [ target ].GetItem () != null ) {
            lastItem = target;
            Fire ();
        }
    }

    public override void Fire () {
        if ( lastItem == -1 || ports [ lastItem ].GetItem () == null ) return;
        base.Fire ();
        if ( transfer != null ) {
            ports [ lastItem ].GetItem ().Autobind ( transfer.GetComponent<ItemPort> () );
            lastItem = -1;
        }
    }

    private void OnDisable () {
        for ( int i = 0; i < ports.Length; i++ ) {
            Eject ( i );
        }
    }
}
