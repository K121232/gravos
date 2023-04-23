using UnityEngine;

public class TargetControlP : TargetControlCore {
    public  ItemPort[]      weaponPorts;
    public  Transform       carrot;

    private bool            transition, delta;

    private void Start () {
        for ( int i = 0; i < weaponPorts.Length; i++ ) {
            weaponPorts [ i ].Autoload ();
            weaponPorts [ i ].attachCallback = RefreshTurretList;
        }
        RefreshTurretList ();
    }

    public  void    RefreshTurretList () {
        if ( turrets.Length != 0 && weaponPorts.Length == 0 ) { return; }
        turrets = new TargetingRig [ weaponPorts.Length ];
        for ( int i = 0; i < weaponPorts.Length; i++ ) {
            if ( turrets [ i ] != null ) turrets [ i ].OverrideTriggerPress ( false );
            if ( weaponPorts [ i ].item != null ) {
                turrets [ i ] = weaponPorts [ i ].item.GetComponent<TargetingRig> ();
            }
        }
        ModifyTarget ( carrot );
    }

    void Update () {
        delta = Input.GetAxis ( "Fire1" ) > 0;
        if ( firingLock ) {
            delta = false;
        }
        if ( delta != transition ) {
            for ( int i = 0; i < turrets.Length; i++ ) {
                if ( turrets [ i ] != null ) {
                    turrets [ i ].OverrideTriggerPress ( delta );
                }
            }
            transition = delta;
        }            
    }

    public override void ModifyLock ( bool alpha ) {
        firingLock = alpha;
    }

    public override void ModifyTurretOp ( TargetingRig alpha ) {
        alpha.fireControlOverride = true;
        base.ModifyTurretOp ( alpha );
    }
}
