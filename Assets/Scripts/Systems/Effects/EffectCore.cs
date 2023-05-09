using UnityEngine;

public class EffectCore : ZephyrUnit {
    public  Transform   target;

    public  float   time;
    private float   deltaTime;

    public  bool    dormant = true;

    public override void Autobind ( ZephyrUnit _unit ) {
        EffectReciever  rx = (EffectReciever) _unit;
        if ( rx != null ) {
            base.Autobind ( _unit );

            target = rx.mainHull;

            CryoStore ( transform );

            dormant = false;
            deltaTime = time;
        }
    }

    public  virtual void    CryoStore ( Transform alpha ) {
        alpha.GetChild ( 0 ).gameObject.SetActive ( false );
        alpha.GetChild ( 1 ).gameObject.SetActive ( false );
    }

    public virtual void Update () {
        if ( deltaTime > 0 ) {
            deltaTime -= Time.deltaTime;
            if ( deltaTime < 0 ) {
                Autobreak ();
            }
        }
    }
}
