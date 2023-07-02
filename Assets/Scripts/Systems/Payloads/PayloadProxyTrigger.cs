using UnityEngine;

public class PayloadProxyTrigger : PayloadCore {
    public Radar    radar;

    private void Update () {
        if ( radar.collectedCount != 0 ) {
            Store ();
        }   
    }
}
