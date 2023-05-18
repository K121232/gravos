using Mono.Cecil.Cil;
using UnityEngine;

public class PayloadDelay : PayloadCore {
    public  float   delay;
    private float   delta = -1;

    public override void Deploy ( PayloadObject _instructions ) {
        delta = delay;
        enabled = true;
        base.Deploy ( _instructions );
    }

    public override void Store () {
        enabled = false;
        base.Store ();
    }

    private void Update () {
        if ( delta > 0 ) {
            delta -= Time.deltaTime;
            if ( delta < 0 ) {
                Store ();
            }
        }
    }
}
