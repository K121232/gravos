using UnityEngine;

// FX Dispenser
public class PayloadFXD : PayloadCore {
    public PoolSpooler  effect;

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        if ( effect != null ) {
            GameObject      deltaGO = effect.Request();
            ParticleSystem  deltaPS = deltaGO.GetComponent<ParticleSystem>();
            deltaGO.SetActive ( true );
            if ( deltaPS != null ) {
                var deltaShape = deltaPS.shape;
                deltaShape.position = transform.position;
                deltaShape.rotation = transform.worldToLocalMatrix * new Vector3 ( 0, 0, ( 180 - deltaPS.shape.arc ) / 2 + Vector2.Angle ( Vector2.up, instructions.heading ) );
                deltaPS.Play ();
            }
        }
        Store ();
    }
}
