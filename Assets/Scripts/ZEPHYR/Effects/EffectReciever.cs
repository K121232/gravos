using UnityEngine;

public class EffectReciever : ZephyrUnit {
    public  Radar       radar;
    public  Transform   mainHull;

    private EffectCore  delta;

    public  LabelNotification datalink;

    void Update () {
        if ( radar.collectedCount != 0 ) {
            for ( int i = 0; i < radar.collectedCount; i++ ) {
                if ( radar.collectedColliders [ i ].transform.parent.TryGetComponent( out delta ) ) {
                    delta.Autobind ( this );
                    if ( datalink != null ) {
                        datalink.AddMessage ( new DataLinkNTF ( " Effect " + delta.effectName + " for " + delta.time + " s ", delta.time ) );
                    }
                }
            }
        }    
    }
}