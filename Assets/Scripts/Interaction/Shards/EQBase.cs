using UnityEngine;

public class EQBase : MonoBehaviour {
    protected   Rigidbody2D rgb;

    private void Start () {
        if ( GetComponent <ItemHandle>() ) {
            GetComponent<ItemHandle> ().onDeltaCallback = MainInit;
        }
    }

    public virtual void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        enabled = port.bungholio;
        rgb = port.hullLink.GetComponent<Rigidbody2D> ();
    }
}
