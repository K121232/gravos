using UnityEngine;

public class Endgoal : MonoBehaviour {
    public  GameObject[]            killTargets;

    public  ProtoPlayerBridge       playerBridge;

    private void Start () {
        for ( int i = 0; i < killTargets.Length; i++ ) {
            if ( !killTargets [ i ].activeInHierarchy ) {
                gameObject.SetActive( false );
            }
        }
    }

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