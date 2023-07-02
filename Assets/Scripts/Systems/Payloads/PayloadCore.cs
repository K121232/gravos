using UnityEngine;

public class PayloadCore : MonoBehaviour {
    public      PayloadCore     next            = null;
    protected   PayloadObject   instructions    = null;

    public virtual void Deploy ( PayloadObject _instructions = null ) {
        if ( instructions == null ) {
            instructions = _instructions;
        }
        //Debug.Log ( "DEPLOYED " + instructions + " BY " + this.GetType () );
        enabled = true;
    }

    public virtual void Store () {
        //Debug.Log ( "STORING " + this.GetType () );
        enabled = false;
        instructions = null;
    }

    public virtual void PassOn () {
        if ( next != null && instructions != null ) {
            next.Deploy ( instructions );
        }
    }

    private void OnDisable () {
        Store ();
    }
}
