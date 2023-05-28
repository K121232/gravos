using UnityEngine;

public class PayloadFireable : FireableCore, INTFCM {
    public  float       minRangeOffset  = 1;
    public  bool        inheritLayer    = true;

    public virtual GameObject Fire () {
        if ( controller == null ) return null;

        GameObject instPayload  = payloadLoader.Request();
        if ( instPayload == null ) return null;
        if ( inheritLayer ) {
            instPayload.layer = gameObject.layer;
        }

        instPayload.transform.SetPositionAndRotation ( 
            transform.position + transform.up * minRangeOffset, 
            transform.rotation 
            );

        instPayload.GetComponent<PayloadStart> ().Deploy (
            new PayloadObject ( 
                controller.GetV (), 
                transform.rotation * controller.aimOffset, 
                controller.target ) 
            );

        instPayload.SetActive ( true );

        if ( trailLoader != null ) {
            GameObject instTrail    = trailLoader.Request();
            instTrail.GetComponent<TrailAddon> ().Bind ( instPayload.transform );
            instTrail.SetActive ( true );
        }

        return instPayload;
    }
}
