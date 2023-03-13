using UnityEngine;

public class FireControlSystem : MonoBehaviour {
    public  Turret[]        turrets;

    public  Radar           perimeterRadar;

    private GameObject      trackedTarget;
    private GameObject      delta;

    void Update () {
        for ( int i = 0; i < perimeterRadar.collectedCount; i++ ) {
            if ( perimeterRadar.collectedColliders [ i ].gameObject == trackedTarget ) {
                return;
            }
        }
        delta = null;
        if ( perimeterRadar.collectedCount != 0 ) {
            delta = perimeterRadar.collectedColliders [ 0 ].gameObject;
        }
        for ( int i = 0; i < turrets.Length; i++ ) {
            turrets [ i ].LoadTarget ( delta );
        }
        trackedTarget = null;
    }
}
