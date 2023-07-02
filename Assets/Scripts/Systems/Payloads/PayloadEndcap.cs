using UnityEngine;

public class PayloadEndcap : PayloadCore {

    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );
        if ( instructions != null ) {
            if ( instructions.controllerRoot != null ) {
                instructions.controllerRoot.gameObject.SetActive ( false );
            }
        }
        Store ();
    }
}
