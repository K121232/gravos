using UnityEngine;

public class PayloadTrailDeleter : PayloadCore {
    public TrailRenderer    trailRenderer;

    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );
        PassOn ();
    }

    public override void Store () {
        if ( trailRenderer != null ) {
            if ( trailRenderer != null ) {
                trailRenderer.Clear ();
            }
        }
        base.Store ();
    }
}
