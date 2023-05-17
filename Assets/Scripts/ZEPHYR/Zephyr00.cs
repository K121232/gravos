using UnityEngine;
using System.Collections.Generic;

public class Zephyr00 : MonoBehaviour {
    public  List<ZephyrUnit>    coan;

    public virtual  void    Attach ( ZephyrUnit alpha ) {
        if ( !coan.Contains ( alpha ) ) {
            coan.Add ( alpha );
        }
        OnAttach ( alpha );
    }

    public virtual void OnAttach ( ZephyrUnit alpha ) {
        
    }

    public virtual void     Detach ( ZephyrUnit alpha ) {
        if ( coan.Contains ( alpha ) ) {
            coan.Remove ( alpha );
        }
        OnDetach ( alpha );
    }

    public virtual void OnDetach ( ZephyrUnit alpha ) {

    }

    protected virtual void Start () {
    }
}
