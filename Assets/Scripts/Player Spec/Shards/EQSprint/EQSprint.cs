using UnityEngine;

public class EQSprint : EQBase {
    [Header ( "EQ Sprint" )]
    public  ParticleSystem  speedlines;
    public  TeflonMovement  movementCore;

    public  float           sprintSTR;
    private float           pastMax;

    public override void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        base.MainInit ( port );
        if ( enabled ) {
            cell = port.batteryLink.GetChild ( 2 ).GetComponent<PowerCell> ();
        } else {
            cell = null;
        }
    }

    public override void OnStartFire () {
        pastMax = movementCore.mxv;
        speedlines.Play ();
    }

    public override void OnStopFire () {
        movementCore.mxv = pastMax;
        speedlines.Stop ();
    }

    public override GameObject Fire () {
        if ( movementCore.mxv != sprintSTR ) {
            pastMax = movementCore.mxv;
            movementCore.mxv = sprintSTR;
        }
        movementCore.AddV ( 1 );
        return base.Fire ();
    }
}
