using UnityEngine;

public class PayloadDelay : PayloadCore {
    public  float       delay;
    protected float     delta = -1;

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        delta = delay;
    }

    private void Update () {
        if ( delta > 0 ) {
            delta -= Time.deltaTime;
            if ( delta < 0 ) {
                PassOn ();
                Store ();
            }
        }
    }
}
