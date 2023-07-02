using UnityEngine;

public class PayloadFollower : PayloadCore {
    public  Transform       target;

    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );
        if ( instructions.target != null ) {
            target = instructions.target;
        }
    }

    public override void Store () {
        base.Store ();
        target = null;
    }

    void LateUpdate () {
        if ( target != null ) {
            transform.position = target.position;
        }
    }
}
