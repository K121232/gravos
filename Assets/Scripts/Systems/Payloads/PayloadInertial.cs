using UnityEngine;

public class PayloadInertial : PayloadCore {
    private Rigidbody2D rgb;
    public  float   launchSpeed;

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        if ( instructions != null ) {
            if ( rgb != null || instructions.controllerRoot.TryGetComponent ( out rgb ) ) {
                rgb.velocity = launchSpeed * instructions.heading.normalized + instructions.hostV;
            }
        }
        PassOn ();
        Store ();
    }
}
