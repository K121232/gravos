using UnityEngine;

//Split execution
public class PayloadSE : PayloadCore {
    public PayloadCore nextSecondary;

    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );
        PassOn ();
        Store ();
    }

    public override void PassOn () {
        if ( instructions != null ) {
            if ( next != null ) {
                next.Deploy ( instructions );
            }
            if ( nextSecondary != null ) {
                nextSecondary.Deploy ( instructions );
            }
        }
    }
}
