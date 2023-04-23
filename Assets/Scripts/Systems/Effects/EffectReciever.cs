using UnityEngine;

public class EffectReciever : MonoBehaviour {
    public  Radar       radar;
    public  Transform   mainHull;

    private EffectCore  delta;
 
    void Update () {
        if ( radar.collectedCount != 0 ) {
            for ( int i = 0; i < radar.collectedCount; i++ ) {
                if ( radar.collectedColliders [ i ].TryGetComponent( out delta ) ) {
                    delta.Activate ( mainHull );
                }
            }
        }    
    }
}