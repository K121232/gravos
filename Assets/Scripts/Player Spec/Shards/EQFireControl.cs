using UnityEngine;

public class EQFireControl : MonoBehaviour {
    public  ItemPort[]      weaponPorts;
    public  TargetingRig[]  turrets;
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
            // Make sure the weapons leave clean
            if ( turrets [ i ] != null ) turrets [ i ].OverrideTriggerPress ( false );
            weaponPorts [ i ].Autoload ();
            if ( weaponPorts [ i ].item != null ) {
                turrets [ i ] = weaponPorts [ i ].item.GetComponent<TargetingRig> ();
            }
        }
        LoadTargets ();
    }

    void Update () {
        delta = Input.GetAxis ( "Fire1" ) > 0;
        if ( delta != transition ) {
            for ( int i = 0; i < turrets.Length; i++ ) {
                if ( turrets [ i ] != null ) {
                    turrets [ i ].OverrideTriggerPress ( delta );
                }
            }
            transition = delta;
        }            
    }

    private void LoadTargets () {
        for ( int i = 0; i < turrets.Length; i++ ) {
            if ( turrets [ i ] != null ) {
                turrets [ i ].LoadTarget ( carrot.gameObject );
                turrets [ i ].fireControlOverride = true;
            }
        }
    }
}
