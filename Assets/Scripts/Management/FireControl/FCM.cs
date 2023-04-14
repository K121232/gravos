using UnityEngine;
using System;

public class FCM : MonoBehaviour {
    // FIRE CONTROL MECHANISM
    protected bool  triggerDown;
    private bool    triggerSear;
    public  bool    canAuto;

    public  float   fireRate;
    protected float   deltaC;

    public  Action  burialHelper = null;

    public virtual void TriggerHold () {
        triggerDown = true;
    }

    public virtual void TriggerRelease () {
        triggerDown = false;
        triggerSear = true;
    }

    public virtual void Reload () {}

    public virtual void Start () {
        deltaC = 0;
    }

    public virtual void OnEnable () {
        triggerSear = true;
        deltaC = 0;
    }

    public virtual bool AmmoCheck() { return true; }

    public virtual void Update () {
        if ( deltaC >= 0 ) {
            deltaC -= Time.deltaTime;
            if ( deltaC < 0 && canAuto ) {
                triggerSear = true;
            }
        }
        if ( deltaC <= 0 && triggerDown && triggerSear && AmmoCheck() ) {
            Fire ();
            deltaC = fireRate;
            triggerSear = false;
        }
    }

    public virtual GameObject Fire () { if ( burialHelper != null ) { burialHelper (); } return null; }
}
