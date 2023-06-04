using UnityEngine;

public class PayloadProxyTrigger : PayloadCore {
    public Radar    radar;

    private void Update () {
        if ( deployed && radar.collectedCount != 0 ) {
            Store ();
        }   
    }
}
