using UnityEngine;

public class TurretAutoTarget : MonoBehaviour {
    public  Turret  turret;
    public  string              targetTag;
    public  Radar               radar;

    private GameObject          pastTarget;

    void Update() {
        if ( radar.collectedCount != 0 ) {
            for ( int i = 0; i < radar.collectedCount; i++ ) {
                if ( radar.collectedColliders[ i ].gameObject.CompareTag( targetTag ) ) {
                    if ( radar.collectedColliders[i].gameObject == pastTarget ) return;
                    pastTarget = radar.collectedColliders[i].gameObject;
                    turret.LoadTarget( pastTarget );
                    return;
                }
            }
        }
        pastTarget = null;
        turret.LoadTarget( null );
    }
}
