using UnityEngine;

public class PowerCell : MonoBehaviour {
    public  float   resourceMax;
    public  float   resourceCurrent;

    public  float   flatRate;
    public  float   loadRate;

    public  float   loadTimeDown;
    public  float   loadTimeDead;
    private float   timeLoad;

    public  bool    recovering;

    // When the ship fully depletes energy, have long cooldown but unleash a big ass wave of energy ( radiators )
    // Maybe an ideea ...

    private void Start() {
        if ( !recovering ) {
            resourceCurrent = resourceMax;
        }
    }

    public bool Available () {
        return !recovering && resourceCurrent > 0;
    }

    void Update() {
        resourceCurrent += flatRate * Time.deltaTime;
        if ( recovering || timeLoad <= 0 ) { resourceCurrent += loadRate * Time.deltaTime; }
        if ( timeLoad > 0 ) {
            timeLoad -= Time.deltaTime;
            if ( timeLoad <= 0 && recovering ) {
                recovering = false;
            }
        }
        if ( resourceCurrent > resourceMax ) {
            resourceCurrent = resourceMax;
        }
    }

    public void Load ( float amount ) {
        resourceCurrent += amount;
        if ( timeLoad > 0 ) {
            timeLoad = loadTimeDown;
            recovering = false;
        }
        if ( resourceCurrent > resourceMax ) {
            resourceCurrent = resourceMax;
        }
    }

    public bool Drain ( float amount ) {
        if ( amount <= 0 ) return true;
        if ( !recovering && resourceCurrent - amount >= 0 ) {
            resourceCurrent -= amount;
            timeLoad = loadTimeDown;
            if ( resourceCurrent <= 0 ) {
                resourceCurrent = 0;
                recovering = true;
                timeLoad = loadTimeDead;
            }
            return true;
        }
        return false;
    }

    public float VariDrain ( float amount ) {
        amount = Mathf.Min ( amount, resourceCurrent );
        if ( recovering ) amount = 0;
        Drain ( amount );
        return amount;
    }
}
