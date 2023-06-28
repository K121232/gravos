using UnityEngine;

public class EQSprint : UnifiedOrdnance {
    [Header ( "EQ Sprint" )]
    public  ParticleSystem  speedlines;
    public  TeflonMovement  movementCore;

    public  float           sprintSTR;
    private float           pastMax = -1;

    public override void MainInit ( Thunder _controller ) {
        base.MainInit ( _controller );
        if ( enabled && controller != null && controller.rgb != null ) {
            movementCore = controller.rgb.GetComponent<TeflonPMove> ();
        }
    }

    public override void OnStartFire () {
        if ( controller == null ) return;
        pastMax = movementCore.mxv;
        if ( speedlines != null ) {
            speedlines.Play ();
        }
    }

    public override void OnStopFire () {
        if ( controller == null ) return;
        if ( pastMax != -1 ) {
            movementCore.mxv = pastMax;
        }
        if ( speedlines != null ) {
            speedlines.Stop ();
        }
    }

    public override void Fire () {
        if ( controller == null ) return;
        if ( movementCore.mxv != sprintSTR ) {
            pastMax = movementCore.mxv;
            movementCore.mxv = sprintSTR;
        }
        movementCore.SetThrusterOutput ( 1 );
    }
}
