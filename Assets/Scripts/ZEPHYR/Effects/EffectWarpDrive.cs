using UnityEngine;

public class EffectWarpDrive : EffectCore {

    private bool    isWarping = false;
    public  float   strength;

    private void ModifyDriveState ( bool _alpha ) {
        isWarping = _alpha;
    }

    public override void Autobind ( ZephyrUnit _unit ) {
        base.Autobind ( _unit );
        if ( !dormant ) {
            ModifyDriveState ( true );
        }
    }

    public override void Autobreak () {
        if ( !dormant ) {
            ModifyDriveState ( false );
        }
        base.Autobreak ();
    }

    public override void Update () {
        if ( isWarping && target != null ) {
            TeflonMovement mvt;
            if ( target.TryGetComponent ( out mvt ) ) {
                mvt.SetThrusterOutput ( strength );
            }
        }
        base.Update ();
    }
}
