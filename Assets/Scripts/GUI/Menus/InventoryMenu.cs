using UnityEngine;

public class InventoryMenu : MenuCore {
    void Update () {
        if ( Input.GetKeyDown ( KeyCode.P ) ) {
            Incoming ( !animator.GetBool ( "Dispatch" ) );
        }
    }
}
