using UnityEngine;

public class EQFireControl : MonoBehaviour {
    public  ItemPort[]      weaponPorts;
    public  TargetingRig[]        turrets;
    public  Transform       carrot;

    private bool            transition, delta;

    private void Start () {
        for ( int i = 0; i < weaponPorts.Length; i++ ) {
            weaponPorts [ i ].onDeltaCallback = RefreshTurretList;
        }
        RefreshTurretList ();
        LoadTargets ();
    }

    public  void    RefreshTurretList () {
        Debug.Log ( "REFRESH THE TURRET LIST" );
        if ( turrets.Length != 0 && weaponPorts.Length == 0 ) { return; }
        turrets = new TargetingRig [ weaponPorts.Length ];
        for ( int i = 0; i < weaponPorts.Length; i++ ) {
            if ( turrets [ i ] != null ) turrets [ i ].OverrideTriggerPress ( false );
            turrets [ i ] = weaponPorts [ i ].transform.GetChild(0).GetComponent<TargetingRig> ();
        }
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
            turrets [ i ].LoadTarget ( carrot.gameObject );
            turrets [ i ].fireControlOverride = true;
        }
    }

    public void AutoUnBind ( TargetingRig a ) {
        if ( a == null ) return;
        a.LoadTarget ( null );
        for ( int i = 0; i < turrets.Length; i++ ) {
            if ( turrets [ i ] == a ) {
                turrets [ i ] = null;
            }
        }
    }

    public void AutoBind ( TargetingRig a ) {
        if ( a == null ) return;
        for ( int i = 0; i < turrets.Length; i++ ) {
            if ( turrets [ i ] == null ) {
                turrets [ i ] = a;
            }
        }
        LoadTargets ();
    }
}
