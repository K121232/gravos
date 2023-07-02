using UnityEngine;

public class PayloadNullSlide : PayloadCore {
    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );
        PassOn ();
        Store ();
    }
}
