using UnityEngine;

public class ZethaMinion : Hitbox {
    public  Zetha   controller;
    public  int     id;

    private void Start () {
        id = controller.Subscribe ( this );
    }

    public override void DeltaF ( int a ) {
        if ( id != -1 && controller != null ) {
            controller.DeltaIncoming ( id, a );
        }
        base.DeltaF ( a );
    }
}
