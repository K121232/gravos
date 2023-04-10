using UnityEngine;

public class Turret : TriggerAssembly {
    public  GameObject  mainHull;
    public  PoolSpooler autoloader;
    public  PoolSpooler trailLoader;

    public  Transform   target;
    public  float       minRangeOffset;

    public  void    SetTarget ( Transform _target ) {
        target = _target;
    }

    public override void TriggerRelease () {
        target = null;
        base.TriggerRelease ();
    }

    public override GameObject Fire ( Vector2 prv ) {
        GameObject instPayload  = autoloader.Request();
        if ( inheritLayer ) {
            instPayload.layer = gameObject.layer;
        }

        instPayload.transform.SetPositionAndRotation ( transform.position + transform.up * minRangeOffset, transform.rotation );

        if ( instPayload.GetComponent<InertialImpactor> () != null ) {
            instPayload.GetComponent<InertialImpactor> ().Prime ( prv );
        }

        if ( instPayload.GetComponent<HSTM> () != null ) {
            instPayload.GetComponent<HSTM> ().Bind ( target );
        }

        if ( instPayload.GetComponent<TMFuse> () != null ) {
            instPayload.GetComponent<TMFuse> ().Bind ( mainHull ? mainHull.transform : transform, prv );
        }

        instPayload.SetActive ( true );

        if ( trailLoader != null ) {
            GameObject instTrail    = trailLoader.Request();
            instTrail.GetComponent<TrailAddon> ().Bind ( instPayload.transform );
            instTrail.SetActive ( true );
        }

        return instPayload;
    }
}
