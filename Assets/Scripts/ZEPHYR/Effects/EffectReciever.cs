using UnityEngine;

public class EffectReciever : ZephyrUnit {
    public  Radar       radar;
    public  Transform   mainHull;

    private EffectCore  delta;

    void Update () {
        if ( radar.collectedCount != 0 ) {
            for ( int i = 0; i < radar.collectedCount; i++ ) {
                if ( radar.collectedColliders [ i ].transform.parent.TryGetComponent( out delta ) ) {
                    delta.Autobind ( this );
                }
            }
        }    
    }
}