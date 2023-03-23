using UnityEngine;

public class PoolSpoolerZetha : PoolSpooler {
    public  PoolSpooler     deathFX;
    public  PoolSpooler     hitboxFX;
    public override void ItemOnStart ( GameObject alpha ) {
        Zetha zetha;
        if ( alpha.TryGetComponent( out zetha ) ) {
            zetha.deathPs = deathFX;
        }
        Hitbox hitbox;
        if ( alpha.TryGetComponent( out hitbox ) ) {
            hitbox.ps = hitboxFX;
        }
        base.ItemOnStart ( alpha );
    }
}
