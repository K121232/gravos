using UnityEngine;

public class PayloadFireable : CoreFireable {
    public  float       minRangeOffset  = 1;
    public  bool        inheritLayer    = true;

    public override void Fire () {
        transfer = null;
        if ( controller == null ) return;
        transfer = payloadLoader.Request ();
        if ( transfer == null ) return;

        if ( inheritLayer ) {
            transfer.layer = gameObject.layer;
            foreach ( Transform t in transfer.GetComponentInChildren<Transform>(includeInactive:true) ) {
                t.gameObject.layer = gameObject.layer;
            }
        }

        transfer.transform.SetPositionAndRotation (
            transform.position + transform.up * minRangeOffset,
            transform.rotation
            );

        transfer.SetActive ( true );

        // Transfer payload start
        PayloadStart tps = transfer.GetComponent<PayloadStart> ();

        tps.Deploy (
            new PayloadObject (
                controller.GetV (),
                transform.rotation * controller.aimOffset,
                controller.target )
            );

        // Expected lifetime
        float exlf = -2;

        if ( trailLoader != null ) {
            GameObject instTrail    = trailLoader.Request();
            instTrail.SetActive ( true );
            instTrail.GetComponent<PayloadStart> ().Deploy (
                new PayloadObject (
                    controller.GetV (),
                    transform.rotation * controller.aimOffset,
                    transfer.transform, null,
                    exlf
                    )
                );
        }
    }
}
