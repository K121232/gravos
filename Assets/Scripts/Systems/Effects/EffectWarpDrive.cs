using UnityEngine;

public class EffectWarpDrive : EffectCore {
    private bool    isWarping = false;
    public  float   strength;

    private void ModifyDriveState ( bool magic ) {
        isWarping = magic;
    }

    public override void OnMagicEngage () {
        if ( !dormant ) {
            ModifyDriveState ( true );
        }
        base.OnMagicEngage ();
    }

    public override void OnMagicDisengage () {
        if ( dormant ) {
            ModifyDriveState ( false );
        }
        base.OnMagicDisengage ();
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
