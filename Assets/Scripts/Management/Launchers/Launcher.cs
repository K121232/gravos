using UnityEngine;

public class Launcher : MonoBehaviour {
    public  PoolSpooler autoLoader;
    public  PoolSpooler trailLoader;

    public  float       spawnChance;
    public  float       safeTime;
    protected float     deltaTime;

    public  float       maxRange;
    public  float       minRange;

    public virtual void Fire () {}

    public virtual void Update() {
        if ( spawnChance <= 0 ) return;
        if ( deltaTime <= 0 ) {
            if ( Random.value < spawnChance ) {
                Fire();
            }
            deltaTime = safeTime;
        } else {
            deltaTime -= Time.deltaTime;
        }
    }
}
