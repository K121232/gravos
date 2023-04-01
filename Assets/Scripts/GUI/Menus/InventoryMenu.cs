using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MenuCore {

    public  Transform           equipmentRoot;

    public  List<ItemHandle>    stores;

    public override void Update () {
        if ( Input.GetKeyDown ( KeyCode.P ) ) {
            Backflow ( !status );
        }
        base.Update ();
    }
}
