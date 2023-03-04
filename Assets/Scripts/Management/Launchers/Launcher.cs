using UnityEngine;

public class Launcher : MonoBehaviour {
    public  PoolSpooler autoLoader;
    public  PoolSpooler trailLoader;

    public  float       spawnChance;
    public  float       safeTime;
    protected float     deltaTime;

    public  float       spawnRange;

    public virtual void Fire () {}

    public virtual void Update() {
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
