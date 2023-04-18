using UnityEngine;
using System;

public class FCM : MonoBehaviour {
    // FIRE CONTROL MECHANISM
    protected bool  triggerDown;
    private bool    triggerSear;
    public  bool    canAuto = true;

    public  float   fireRate;
    protected float   deltaC;

    public  Action  burialHelper = null;
    public  bool    isFiring;

    public virtual void TriggerHold () {
        triggerDown = true;
    }

    public virtual void TriggerRelease () {
        triggerDown = false;
        triggerSear = true;
    }

    public virtual void Reload () {}

    public virtual void Start () {
        isFiring = false;
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
            deltaC = fireRate;
            triggerSear = false;
            if ( !isFiring ) {
                OnStartFire ();
            }
            isFiring = true;
            Fire ();
        } else {
            if ( isFiring ) {
                OnStopFire ();
            }
            isFiring = false;
        }
    }

    public virtual void OnStartFire () {}
    public virtual void OnStopFire () {}
    public virtual GameObject Fire () { if ( burialHelper != null ) { burialHelper (); } return null; }

    public  float   GetCooldownProgress () {
        if ( fireRate == 0 || deltaC <= 0 ) return 1;
        return ( fireRate - deltaC ) / fireRate;
    }
}
