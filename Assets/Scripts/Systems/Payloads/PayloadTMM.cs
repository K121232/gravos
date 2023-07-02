using UnityEngine;

// Targeting module manipulator
public class PayloadTMM : PayloadCore {
    private TM  tm;

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        if ( instructions != null ) {
            if ( tm == null ) {
                tm = instructions.controllerRoot.GetComponent<TM> ();
            }
            if ( tm is HSTM ) {
                ( tm as HSTM ).Bind ( instructions.target );
            }
            tm.enabled = true;
        }
        PassOn ();
    }

    public override void Store () {
        if ( tm == null ) {
            tm = GetComponent<TM> ();
        }
        tm.enabled = false;
        base.Store ();
    }
}
