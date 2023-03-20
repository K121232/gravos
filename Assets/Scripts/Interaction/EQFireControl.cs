using UnityEngine;

public class EQFireControl : MonoBehaviour {
    public  TargetingRig[]  turrets;
    public  Transform       carrot;

    private bool            transition, delta;

    private void Start () {
        for ( int i = 0; i < turrets.Length; i++ ) {
            turrets [ i ].LoadTarget ( carrot.gameObject );
            turrets [ i ].fireControlOverride = true;
        }
    }

    void Update () {
        delta = Input.GetAxis ( "Fire1" ) > 0;
        if ( delta != transition ) {
            for ( int i = 0; i < turrets.Length; i++ ) {
                turrets [ i ].OverrideTriggerPress ( delta );
            }
            transition = delta;
        }            
    }
}
