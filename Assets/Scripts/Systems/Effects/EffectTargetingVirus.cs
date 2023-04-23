using UnityEngine;

public class EffectTargetingVirus : EffectCore {
    private void MassModifyLocks ( bool a ) {
        if ( target == null ) return;
        TargetControlCore[] delta;
        delta = target.GetChild ( 2 ).GetComponents<TargetControlCore> ();
        for ( int i = 0; i < delta.Length; i++ ) {
            delta [ i ].ModifyLock ( a );
        }
    }

    public override void OnEnable () {
        if ( !dormant ) {
            MassModifyLocks ( true );
        }
        base.OnEnable ();
    }

    public override void OnDisable () {
        if ( dormant ) {
            MassModifyLocks ( false );
        }
        base.OnDisable ();
    }
}
