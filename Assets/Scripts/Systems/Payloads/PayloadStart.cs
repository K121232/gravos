using UnityEngine;

public class PayloadStart : PayloadCore {
    public  PayloadCore earlyDetonationTarget;
    private PayloadObject staleInstructions;

    public void EarlyDetonate () {
        if ( staleInstructions != null && earlyDetonationTarget != null ) {
            earlyDetonationTarget.Deploy ( staleInstructions );
        }
        staleInstructions = null;
    }

    private void OnEnable () {
        if ( instructions != null ) {
            Deploy ( instructions );
        }
    }

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        staleInstructions = instructions;
        Store ();
    }

    public override void Store () {
        base.Store ();
    }
}
