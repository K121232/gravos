using UnityEngine;

public class MissileLauncher : Launcher {
    public  GameObject  target;
    public  float       minRange;
    public  Transform   tracker;
    public override void Fire () {
        GameObject delta = autoLoader.Request();
        GameObject trail = trailLoader.Request();

        trail.GetComponent<TrailAddon> ().Bind ( delta.transform );
        delta.GetComponent<HSTM> ().Bind ( target.transform );

        Vector3 deltaUC = Random.insideUnitCircle;
        delta.transform.SetPositionAndRotation ( tracker.position + ( deltaUC * spawnRange + deltaUC.normalized * minRange ), Quaternion.Euler ( 0, 0, 360 * Random.value ) );

        delta.SetActive ( true );
        trail.SetActive ( true );
    }
}
 