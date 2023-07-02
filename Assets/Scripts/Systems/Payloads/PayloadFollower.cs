using UnityEngine;

public class PayloadFollower : PayloadCore {
    protected Transform         target;
    protected Transform         moveTarget;

    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );

        moveTarget = transform;
        if ( instructions != null ) {
            if ( instructions.target != null ) {
                target = instructions.target;
            }
            if ( instructions.controllerRoot != null ) {
                moveTarget = instructions.controllerRoot;
            }
        } else {
            Store ();
        }

        PassOn ();
    }

    public override void Store () {
        base.Store ();
        target      = null;
        moveTarget  = null;
    }

    void LateUpdate () {
        if ( target != null ) {
            moveTarget.position = target.position;
        }
    }
}
