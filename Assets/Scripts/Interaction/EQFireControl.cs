using UnityEngine;

public class EQFireControl : MonoBehaviour {
    public  Camera          cam;
    public  Turret[]        turrets;
    public  Transform       carrot;

    private void Start () {
        for ( int i = 0; i < turrets.Length; i++ ) {
            turrets [ i ].LoadTarget ( carrot.gameObject );
            turrets [ i ].fireControlOverride = true;
        }
    }

    private void LateUpdate () {
        Vector3 delta = cam.ScreenToWorldPoint ( Input.mousePosition );
        delta.z = 0;
        carrot.position = delta;
        carrot.rotation = transform.rotation;
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
