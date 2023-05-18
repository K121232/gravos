using Mono.Cecil.Cil;
using UnityEngine;

// Targeting module manipulator
public class PayloadTMM : PayloadCore {
    private TM  tm;

    public override void Deploy ( PayloadObject _instructions ) {
        if ( tm == null ) {
            tm = GetComponent<TM> ();
        }
        if ( tm is HSTM ) {
            ( tm as HSTM ).Bind ( _instructions.target );
        }
        tm.enabled = true;
        base.Deploy ( _instructions );
    }

    public override void Store () {
        if ( tm == null ) {
            tm = GetComponent<TM> ();
        }
        tm.enabled = false;
        base.Store ();
    }
}
