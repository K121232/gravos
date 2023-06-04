using UnityEngine;

public class PayloadCore : MonoBehaviour {
    public  PayloadCore     next            = null;
    public  bool            deployed        = false;
    protected PayloadObject   instructions  = null;

    public virtual void Deploy ( PayloadObject _instructions ) {
        instructions = _instructions;
        deployed = true;
    }

    public virtual void Store () {
        deployed = false;
        if ( next != null && instructions != null ) {
            next.Deploy ( instructions );
        }
        instructions = null;
    }
}
