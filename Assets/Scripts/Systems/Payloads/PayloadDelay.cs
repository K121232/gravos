using UnityEngine;

public class PayloadDelay : PayloadCore {
    public  float   delay;
    private float   delta = -1;

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        delta = delay;
    }

    private void Update () {
        if ( deployed && delta > 0 ) {
            delta -= Time.deltaTime;
            if ( delta < 0 ) {
                Store ();
            }
        }
    }
}
