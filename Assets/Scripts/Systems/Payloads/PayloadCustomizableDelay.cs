using UnityEngine;

public class PayloadCustomizableDelay : PayloadDelay {

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        if ( instructions != null ) {
            if ( instructions.expectedLifetime != -2 ) {
                delta = instructions.expectedLifetime;
            }
            TrailRenderer trr =  instructions.controllerRoot.GetChild( 0 ).GetComponentInChildren<TrailRenderer>();
            if ( trr != null && delta != -1 ) {
                delta += trr.time;
            }
        }
    }
}
