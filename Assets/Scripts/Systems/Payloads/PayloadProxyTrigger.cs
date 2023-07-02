using UnityEngine;

public class PayloadProxyTrigger : PayloadCore {
    public Radar    radar;

    private void Update () {
        if ( radar != null && radar.collectedCount != 0 ) {
            PassOn ();
            Store ();
        }
    }
}
