using UnityEngine;

public class ZephyrUnit : MonoBehaviour {
    public  ZephyrUnit          mirror;

    public virtual void Autobind ( ZephyrUnit _unit ) {
        if ( _unit == null && mirror != null ) {
            Autobreak ();
        }
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
    }

    protected virtual void AutoloadCore () { }

    public void Autoload () {
        if ( mirror == null ) {
            AutoloadCore ();
        }
        if ( mirror == null || ( mirror != null && mirror.mirror != this ) ) {
            Autobind ( mirror );
        }
    }

    protected virtual void Start () {
        Autoload ();
    }
}
