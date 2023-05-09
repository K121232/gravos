using UnityEngine;

public class ZephyrUnit : MonoBehaviour {
    public  ZephyrUnit          mirror;

    public virtual void Autobind ( ZephyrUnit _unit ) {
        mirror = _unit;
        if ( mirror != null && mirror.mirror != this ) {
            mirror.Autobind ( this );
        }
    }

    public virtual void Autobreak () {
        if ( mirror != null ) {
            ZephyrUnit  delta = mirror;
            mirror = null;
            delta.Autobreak ();
        }
        mirror = null;
    }

    protected virtual void AutoloadCore () { }

    public void Autoload () {
        if ( mirror == null ) {
            AutoloadCore ();
        }
        Autobind ( mirror );
    }

    protected virtual void Start () {
        Autoload ();
    }
}
