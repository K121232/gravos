using UnityEngine;

// FX Dispenser
public class PayloadFXD : PayloadCore {
    public PoolSpooler  explosionEffect;

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        if ( explosionEffect != null ) {
            GameObject      deltaGO = explosionEffect.Request();
            ParticleSystem  deltaPS = deltaGO.GetComponent<ParticleSystem>();
            deltaGO.SetActive ( true );
            if ( deltaPS != null ) {
                var deltaShape = deltaPS.shape;
                deltaShape.position = transform.position;
                deltaPS.Play ();
            }
        }
        Store ();
    }
}
