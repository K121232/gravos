using UnityEngine;

public class PayloadCore : MonoBehaviour {
    public  PayloadCore     next            = null;
    protected PayloadObject   instructions  = null;

    private void Start () {
        enabled = false;
    }

    public virtual void Deploy ( PayloadObject _instructions = null ) {
        if ( instructions == null ) {
            instructions = _instructions;
        }
        //Debug.Log ( "TRIGGERED CORE, STATUS: " + gameObject.activeInHierarchy );
        enabled = true;
    }

    public virtual void Store () {
        enabled = false;
        if ( next != null && instructions != null ) {
            next.Deploy ( instructions );
        } else {
            gameObject.SetActive ( false );
        }
        instructions = null;
    }

    private void OnDisable () {
        instructions = null;
    }
}
