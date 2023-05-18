using UnityEngine;

public class PayloadCore : MonoBehaviour {
    public  PayloadCore     next;
    public  bool            deployed;
    protected PayloadObject   instructions;

    public virtual void Deploy ( PayloadObject _instructions ) {
        instructions = _instructions;
        deployed = true;
    }

    public virtual void Store () {
        deployed = false;
        if ( next != null && instructions != null ) {
            next.Deploy ( instructions );
        }
    }

    private void OnDisable () {
        Store ();
    }
}
