using UnityEngine;

public class EffectTargetingVirus : EffectCore {
    private void MassModifyLocks ( bool a ) {
        if ( target == null ) return;
        Zeus[] delta;
        delta = target.transform.GetChild ( 2 ).GetComponents<Zeus> ();
        for ( int i = 0; i < delta.Length; i++ ) {
            delta [ i ].SetFiringLock ( a );
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
