using UnityEngine;

public class EffectCore : MonoBehaviour {
    protected Transform   target;

    public  float   time;
    private float   deltaTime;

    public  bool    isAbsorbed = true;
    public  bool    dormant = true;

    public  string  signature;

    public  virtual void    Defuse () {
        dormant = true;
        OnMagicDisengage ();
        gameObject.SetActive ( false );
    }

    public  virtual void    Activate ( Transform mainHull ) {
        target = mainHull;
        if ( isAbsorbed ) {
            FinalActivation ( gameObject );
        } else {
            GameObject clone = Instantiate ( gameObject, mainHull.GetChild ( 5 ).GetChild ( 2 ));
            FinalActivation ( clone );
        }
    }

    public virtual  void    FinalActivation ( GameObject obj ) {
        CryoStore ( obj.transform );

        EffectCore efc = obj.GetComponent<EffectCore>();        
        efc.dormant = false;
        efc.deltaTime = time;
        efc.isAbsorbed = false;

        efc.OnMagicEngage ();
    }

    public  virtual void    CryoStore ( Transform alpha ) {
        alpha.GetChild ( 0 ).gameObject.SetActive ( false );
        alpha.GetChild ( 1 ).gameObject.SetActive ( false );
    }

    public virtual  void OnMagicEngage () {

    }

    public virtual void OnMagicDisengage () {
        target = null;
    }

    public virtual void Update () {
        if ( deltaTime > 0 ) {
            deltaTime -= Time.deltaTime;
            if ( deltaTime < 0 ) {
                Defuse ();
            }
        }
    }
}
