using UnityEngine;

public class EQFireControl : MonoBehaviour {
    public  Turret[]        turrets;
    public  Transform       carrot;

    private void Start () {
        for ( int i = 0; i < turrets.Length; i++ ) {
            turrets [ i ].LoadTarget ( carrot.gameObject );
            turrets [ i ].fireControlOverride = true;
        }
    }

    void Update () {
        if ( Input.GetMouseButtonDown( 0 ) ) {
            for ( int i = 0; i < turrets.Length; i++ ) {
                turrets [ i ].TriggerHold ();
            }
        }
        if ( Input.GetMouseButtonUp ( 0 ) ) {
            for ( int i = 0; i < turrets.Length; i++ ) {
                turrets [ i ].TriggerRelease ();
            }
        }
    }
}
