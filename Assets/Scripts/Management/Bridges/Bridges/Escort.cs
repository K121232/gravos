using UnityEngine;

public class Escort : Zetha {
    public  LRCTM       guardPatternTM;
    public  LRCTM       chasePatternTM;

    public  Transform   protectTarget;

    public  Radar       agroRadar;
    public  float       maxChaseRange;

    private bool        chasing;

    public void PhaseChange ( bool _chasing, Transform  _target = null ) {
        chasing = _chasing;
        if ( chasing ) {
            chasePatternTM.LoadCenter ( _target );
        }
        if ( chasePatternTM == guardPatternTM ) return;
        chasePatternTM.enabled = chasing;
        guardPatternTM.enabled = !chasing;
    }

    public override void Start () {
        guardPatternTM = GetComponent<LRCTM> ();
        
        guardPatternTM.LoadCenter ( protectTarget );
        chasePatternTM.LoadCenter ( null );

        chasing = false;
        base.Start ();
    }

    private void Update () {
        if ( ( transform.position - protectTarget.position ).magnitude > maxChaseRange ) {
            if ( chasing ) {
                PhaseChange ( false );
            }
        } else {
            if ( !chasing && agroRadar.collectedCount != 0 ) {
                PhaseChange ( true, agroRadar.collectedColliders [ 0 ].transform );
            }
        }
    }

}
