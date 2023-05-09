using UnityEngine;

public class EffectWarpDrive : EffectCore {
    private bool    isWarping = false;
    public  float   strength;

    private void ModifyDriveState ( bool magic ) {
        isWarping = magic;
    }

    public override void Autobind ( ZephyrUnit _unit ) {
        base.Autobind ( _unit );
        if ( !dormant ) {
            ModifyDriveState ( true );
        }
    }

    public override void Autobreak () {
        if ( dormant ) {
            ModifyDriveState ( false );
        }
        base.Autobreak ();
    }

    public override void Update () {
        if ( isWarping && target != null ) {
            Movement mv;
            if ( target.TryGetComponent( out mv )  ) {
                mv.SetV ( strength );
            }
            TeflonMovement mvt;
            if ( target.TryGetComponent ( out mvt ) ) {
                mvt.SetV ( strength );
            }
        }
        base.Update ();
    }
}
