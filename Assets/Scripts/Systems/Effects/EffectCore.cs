using UnityEngine;

public class EffectCore : MonoBehaviour {
    protected Transform   target;

    public  float   time;
    private float   deltaTime;

    public  bool    isAbsorbed = true;
    public  bool    dormant = true;

    public  virtual void    Defuse () {
        dormant = true;
        if ( !gameObject.activeInHierarchy ) {
            OnDisable ();
        }
        gameObject.SetActive ( false );
    }

    public  virtual void    Activate ( Transform mainHull ) {
        target = mainHull;
        dormant = false;
        deltaTime = time;
        if ( isAbsorbed ) {
            CryoStore ( transform );
        } else {
            GameObject clone = Instantiate ( gameObject, mainHull.GetChild ( 5 ).GetChild ( 2 ));
            CryoStore ( clone.transform );
        }
        if ( gameObject.activeInHierarchy ) {
            OnEnable ();
        }
        gameObject.SetActive ( true );

    }

    public  virtual void    CryoStore ( Transform alpha ) {
        alpha.GetChild ( 0 ).gameObject.SetActive ( false );
    }

    public virtual  void OnEnable () {

    }

    public virtual void OnDisable () {
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
