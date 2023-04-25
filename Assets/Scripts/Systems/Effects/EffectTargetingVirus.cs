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

    public override void OnMagicEngage () {
        if ( !dormant ) {
            MassModifyLocks ( true );
        }
        base.OnMagicEngage ();
    }

    public override void OnMagicDisengage () {
        if ( dormant ) {
            MassModifyLocks ( false );
        }
        base.OnMagicDisengage ();
    }
}
