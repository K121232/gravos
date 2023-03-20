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
        GameObject instTrail    = trailLoader.Request();
        instPayload.layer = gameObject.layer;

        instTrail.GetComponent<TrailAddon> ().Bind ( instPayload.transform );
        Vector3 minHullClearance = transform.position + transform.up * minRangeOffset;

        if ( instPayload.GetComponent<InertialImpactor> () != null ) {
            instPayload.GetComponent<InertialImpactor> ().Prime ( rgb );
        }

        if ( instPayload.GetComponent<HSTM> () != null ) {
            instPayload.GetComponent<HSTM> ().Bind ( target );
        }

        instPayload.transform.SetPositionAndRotation ( minHullClearance, transform.rotation );

        instPayload.SetActive ( true );
        instTrail.SetActive ( true );

        return instPayload;
    }
}
