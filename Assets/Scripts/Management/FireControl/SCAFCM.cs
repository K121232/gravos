using UnityEngine;

public class SCAFCM : FCM {
    // SELF CONTAINED AMMO
    public  float       cooldownReload;
    public  bool        autoreload;

    public  float       reserve = -1;
    public  float       shotCreditBase = 1;
    protected float     shotCredit;
    public  float       shotCost = 1;
    public override void Start () {
        shotCredit = shotCreditBase;
        base.Start (); 
    }

    public override void Update () {
        base.Update ();
        if ( !AmmoCheck () && autoreload ) {
            Reload ();
        }
    }

    public override bool AmmoCheck () {
        return shotCredit >= shotCost;
    }

    override public void Reload () {
        if ( reserve == -1 ) {
            shotCredit = shotCreditBase;
        }
        if ( reserve > 0 ) {
            reserve += shotCredit;
            shotCredit = Mathf.Clamp ( reserve, 0, shotCreditBase );
            reserve -= shotCredit;
        }
        if ( deltaC < 0 ) deltaC = 0;
        deltaC += cooldownReload;
    }

    public override GameObject Fire () {
        shotCredit -= shotCost;
        return base.Fire ();
    }

    public virtual float GetAmmo () {
        if ( shotCost == 0 ) return 999;
        return shotCredit / shotCost;
    }

    public virtual float GetPercentLoad () {
        if ( shotCreditBase == 0 ) return 0;
        return shotCredit / shotCreditBase;
    }
}
