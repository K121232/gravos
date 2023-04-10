using UnityEngine;

public class TriggerAssembly : MonoBehaviour {
    protected   Rigidbody2D     rgb;

    protected bool  triggerDown;
    private bool    triggerSear;
    public  bool    canAuto;

    public  float   fireRate;
    public  float   cooldownReload;
    private float   deltaC;

    public  float   reserve = -1;
    public  float   clip;
    public  float   clipDrain;
    protected float   deltaA;

    public  bool    autoreload;
    public  bool    inheritLayer = true;

    public void SetRGB ( Rigidbody2D alpha ) {
        rgb = alpha;
    }

    public virtual void TriggerHold () {
        triggerDown = true;
    }

    public virtual void TriggerRelease () {
        triggerDown = false;
        triggerSear = true;
    }

    public virtual void Reload () {
        if ( reserve == -1 ) {
            deltaA = clip;
            deltaC = cooldownReload;
        }
        if ( reserve >= 0 ) {
            reserve += deltaA;
            deltaA = Mathf.Clamp ( reserve, 0, clip );
            deltaC = cooldownReload;
            reserve -= deltaA;
            if ( reserve < 0 ) {
                reserve = -2;
            }
        }
    }

    public virtual void Start () {
        deltaA = clip;
        deltaC = 0;
    }

    public virtual void OnEnable () {
        triggerSear = true;
        deltaC = fireRate;
    }

    public virtual void Update () {
        if ( deltaC >= 0 ) {
            deltaC -= Time.deltaTime;
            if ( deltaC < 0 && canAuto ) {
                triggerSear = true;
            }
        }
        if ( deltaC < 0 && triggerDown && triggerSear && deltaA >= clipDrain ) {
            if ( rgb == null ) {
                Fire ( Vector2.zero );
            } else {
                Fire ( rgb.velocity );
            }
            deltaC = fireRate;
            deltaA -= clipDrain;
            if ( deltaA <= 0 && autoreload ) {
                Reload ();
            }
            triggerSear = false;
        }
    }

    public virtual GameObject Fire ( Vector2 prv ) { return null; }

    public virtual float GetAmmo () {
        if ( clipDrain == 0 ) return 999;
        return deltaA / clipDrain;
    }

    public virtual float GetPercentLoad () {
        if ( clip == 0 ) return 0;
        return deltaA / clip;
    }
}
