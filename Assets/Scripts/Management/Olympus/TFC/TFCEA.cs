using UnityEngine;

// Error angle 
public class TFCEA : TFC {
    public      float       coneMaxDeviation = 5;

    protected void Update () {
        if ( controller == null ) return;
        controller.ForceFire (
            controller.target != null && (
                coneMaxDeviation >= 180 ||
                Mathf.Abs ( controller.GetAIMDeviation () ) <= coneMaxDeviation
            )
        );
    }

    public override float GetProgress () {
        if ( controller.target == null ) return 0;
        if ( coneMaxDeviation != 0 ) {
            if ( Mathf.Abs ( controller.GetAIMDeviation () ) < coneMaxDeviation ) {
                return 1;
            }
            return Mathf.Clamp ( 1 - controller.GetAIMTTT (), 0, 1 );
        }
        return 1;
    }
}
