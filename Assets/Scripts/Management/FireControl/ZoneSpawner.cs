using UnityEngine;

public class ZoneSpawner : Turret {
    public  float   maxRange;
    public  float   minRange;

    public  float   chance;
    public  bool    linkBurstChance;
    private bool    burstHold;

    public override void Start () {
        canAuto = true;
        autoreload = true;
        TriggerHold ();
        base.Start ();
    }

    public override GameObject Fire ( Vector2 prv ) {
        if ( Random.Range ( 0, 100 ) / 100 <= chance || burstHold ) {
            if ( linkBurstChance ) {
                burstHold = true;
            }
            GameObject handle = base.Fire ( prv );
            handle.transform.position = transform.position + (Vector3) Random.insideUnitCircle.normalized * Random.Range ( minRange, maxRange );
        }
        return null;
    }

    public override void Reload () {
        burstHold = false;
        base.Reload ();
    }
}
