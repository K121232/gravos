using UnityEngine;

public class ZethaMinion : Hitbox {
    public  Zetha   controller;
    public  int     id;

    public override void Start () {
        id = controller.Subscribe ( this );
        base.Start ();
    }

    public override void DeltaF ( int a ) {
        if ( id != -1 && controller != null ) {
            controller.DeltaIncoming ( id, a );
        }
        base.DeltaF ( a );
    }
}
