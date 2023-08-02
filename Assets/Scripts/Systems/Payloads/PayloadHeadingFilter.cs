using UnityEngine;

public class PayloadHeadingFilter : PayloadCore {
    public float  filterRotation;

    public override void Deploy ( PayloadObject _instructions = null ) {
        base.Deploy ( _instructions );
        if ( instructions != null ) {
            instructions.heading = Quaternion.Euler ( 0, 0, filterRotation ) * instructions.heading;
        }
        PassOn ();
        Store ();
    }
}
