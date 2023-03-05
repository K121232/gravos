using UnityEngine;

public class MissileLauncher : Launcher {
    public  GameObject  target;
    public  Transform   tracker;

    public override void Fire () {
        if ( target == null ) return;

        GameObject delta = autoLoader.Request();
        delta.layer = transform.gameObject.layer;
        GameObject trail = trailLoader.Request();

        trail.GetComponent<TrailAddon> ().Bind ( delta.transform );
        delta.GetComponent<HSTM> ().Bind ( target.transform );

        Vector3 deltaUC = ( target.transform.position - tracker.position ).normalized;
        delta.transform.SetPositionAndRotation ( tracker.position + ( (Vector3)Random.insideUnitCircle * maxRange + deltaUC * minRange ), tracker.rotation );

        delta.SetActive ( true );
        trail.SetActive ( true );
    }
}
 