using UnityEngine;

public class ZephyrUnit : MonoBehaviour {
    public  ZephyrUnit          bind;

    public virtual void Autobinding ( ZephyrUnit _unit ) {
        bind = _unit;
        if ( bind != null && bind.bind != this ) {
            Seal ();
        }
    }

    public virtual  void Seal () {
        bind.Autobinding ( this );
    }

    public  virtual void    Autobreak () {
        if ( bind != null ) {
            Unseal ();
        }
    }

    public  virtual void    Unseal () {
        bind.Autobreak ();
        bind = null;
    }

    public void Autoload () {
        if ( bind == null ) {
            AutoloadCore ();
        }
        Autobinding ( bind );
    }

    protected   virtual void    AutoloadCore () {
    }

    protected virtual void Start () {
        Autoload ();
    }
}
