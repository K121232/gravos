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

        transfer.GetComponent<PayloadStart> ().Deploy (
            new PayloadObject (
                controller.GetV (),
                transform.rotation * controller.aimOffset,
                controller.target )
            );

        if ( trailLoader != null ) {
            GameObject instTrail    = trailLoader.Request();
            instTrail.GetComponent<TrailAddon> ().Bind ( transfer.transform );
            instTrail.SetActive ( true );
        }
    }
}
