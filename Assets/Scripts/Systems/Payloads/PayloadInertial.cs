using UnityEngine;

public class PayloadInertial : PayloadCore {
    private Rigidbody2D rgb;

    public  float   launchSpeed;

    public override void Deploy ( PayloadObject _instructions ) {
        if ( _instructions == null ) Debug.LogError ( "BAD INSTRUCTIONS" );
        if ( rgb != null || TryGetComponent ( out rgb ) ) {
            rgb.velocity = launchSpeed * _instructions.heading.normalized + _instructions.hostV;
        }
        base.Deploy ( _instructions );
        base.Store ();
    }
}
