using UnityEngine;

public class PayloadCore : MonoBehaviour {
    public  PayloadCore     next            = null;
    public  bool            deployed        = false;
    protected PayloadObject   instructions  = null;

    public virtual void Deploy ( PayloadObject _instructions = null ) {
        if ( instructions == null ) {
            instructions = _instructions;
        }
        //Debug.Log ( "TRIGGERED CORE, STATUS: " + gameObject.activeInHierarchy );
        deployed = true;
    }

    public virtual void Store () {
        deployed = false;
        if ( next != null && instructions != null ) {
            next.Deploy ( instructions );
        }
        instructions = null;
    }

    private void OnDisable () {
        instructions = null;
        deployed = false;
    }
}
