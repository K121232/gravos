using UnityEngine;

public class Endgoal : MonoBehaviour {
    public  GameObject[]            killTargets;

    public  ProtoPlayerBridge       playerBridge;

    void Update () {
        int copium = 0;
        for ( int i = 0; i < killTargets.Length; i++ ) {
            if ( !killTargets [ i ].activeInHierarchy ) {
                copium++;
            }
        }
        if ( copium == killTargets.Length ) {
            playerBridge.Detach ( "YOU WON!" );
        }
    }
}