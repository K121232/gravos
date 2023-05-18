using UnityEngine;

public class PayloadStart : PayloadCore {
    private void OnEnable () {
        if ( instructions != null) {
            Deploy ( instructions );
        }
    }

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        Store ();
    }
}
