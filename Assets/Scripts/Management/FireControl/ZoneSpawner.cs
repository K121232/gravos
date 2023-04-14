using UnityEngine;

public class ZoneSpawner : Turret {
    public  float   maxRange;
    public  float   minRange;
    public  float   chance;

    public override void Start () {
        base.Start ();
        fcm.canAuto = true;
        fcm.TriggerHold ();
    }

    public override GameObject Fire () {
        if ( Random.Range ( 0, 100 ) / 100 <= chance ) {
            GameObject handle = base.Fire ();
            handle.transform.position = transform.position + (Vector3) Random.insideUnitCircle.normalized * Random.Range ( minRange, maxRange );
        }
        return null;
    }
}
