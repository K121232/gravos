using UnityEngine;

public class EffectTargetingVirus : EffectCore {
    private void MassModifyLocks ( bool a ) {
        if ( target == null ) return;
        TargetControlCore[] delta;
        delta = target.transform.GetChild ( 2 ).GetComponents<TargetControlCore> ();
        for ( int i = 0; i < delta.Length; i++ ) {
            delta [ i ].ModifyLock ( a );
        }
    }

    public override void Autobind ( ZephyrUnit _unit ) {
        base.Autobind ( _unit );
        if ( !dormant ) {
            MassModifyLocks ( true );
        }
    }

    public override void Autobreak () {
        base.Autobreak ();
        if ( dormant ) {
            MassModifyLocks ( false );
        }
        target = null;
    }
}
