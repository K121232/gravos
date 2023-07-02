using UnityEngine;

public class PayloadTrailDeleter : PayloadCore {
    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );
        PassOn ();
    }

    public override void Store () {
        if ( instructions != null ) {
            TrailRenderer trr =  instructions.controllerRoot.GetChild( 0 ).GetComponentInChildren<TrailRenderer>();
            if ( trr != null ) {
                trr.Clear ();
            }
        }
        base.Store ();
    }
}
