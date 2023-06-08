using UnityEngine;

// Player target control

public class ZeusPTC : Zeus {
    public  ItemPort[]      weaponPorts;
    public  Transform       carrot;

    private bool            transition, delta;

    public string           firingAxis = "Fire1";

    private new void Start () {
        for ( int i = 0; i < weaponPorts.Length; i++ ) {
            weaponPorts [ i ].Autoload ();
        }
        RefreshTurretList ();
    }

    public void AdditionalEquipmentTargetOverride () {
        RefreshTurretList();
        ModifyTarget ( carrot );
    }

    public void RefreshTurretList () {
        if ( turrets.Length != 0 && weaponPorts.Length == 0 ) { return; }

        for ( int i = 0; i < turrets.Length; i++ ) {
            if ( turrets [ i ] != null ) turrets [ i ].ForceFire ( false );
        }
        turrets = new Thunder [ weaponPorts.Length ];
        for ( int i = 0; i < weaponPorts.Length; i++ ) {
            weaponPorts [ i ].Autoload ();
            if ( weaponPorts [ i ].GetItem () != null ) {
                turrets [ i ] = weaponPorts [ i ].GetItem ().GetComponent<Thunder> ();
            }
        }
        ModifyTarget ( carrot );
    }

    void Update () {
        delta = Input.GetAxis ( firingAxis ) > 0;
        if ( firingLock ) {
            delta = false;
        }
        if ( delta != transition ) {
            for ( int i = 0; i < turrets.Length; i++ ) {
                if ( turrets [ i ] != null ) {
                    turrets [ i ].ForceFire ( delta );
                }
            }
            transition = delta;
        }            
    }

    public override void SetFiringLock ( bool alpha ) {
        firingLock = alpha;
    }

    public override void InternalTurretMOP ( Transform target, Thunder alpha ) {
        alpha.SetAutofire ( false );
        base.InternalTurretMOP ( target, alpha );
    }
}
