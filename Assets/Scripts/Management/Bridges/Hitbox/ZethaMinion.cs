using UnityEngine;

public class ZethaMinion : Hitbox {
    public  Zetha   controller;
    public  int     id;

    public void Subscribe ( Zetha _control, int _id ) {
        id = _id;
        controller = _control;
    }

    public override void DeltaF ( int a ) {
        if ( id != -1 && controller != null ) {
            controller.DeltaIncoming ( id, a );
        }
        base.DeltaF ( a );
    }
}
