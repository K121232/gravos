using UnityEngine;

public class PayloadStart : PayloadCore {
    public  PayloadCore     earlyDetonationTarget;
    private PayloadObject   staleInstructions;

    public  Transform       controllerRoot = null;

    public void EarlyDetonate () {
        if ( staleInstructions != null && earlyDetonationTarget != null ) {
            earlyDetonationTarget.Deploy ( staleInstructions );
        }
        staleInstructions = null;
    }

    public override void Store () {
        base.Store ();
        staleInstructions = null;
    }

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        if ( controllerRoot == null ) {
            controllerRoot = transform;
        }
        instructions.InjectCR ( controllerRoot );
        staleInstructions = instructions;
        PassOn ();
        Store ();
    }
}
