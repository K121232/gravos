using UnityEngine;

public class ZethaPatrol : Zetha {
    public  LRCTM       guardPatternTM;
    public  TM          chasePatternTM;

    public  Radar       agroRadar;

    public  Transform   chaseAnchor;
    public  float       maxChaseRange;

    private bool        chasing;

    public void PhaseChange ( bool _chasing, Transform  _target = null ) {
        chasing = _chasing;
        if ( chasing ) {
            if ( chasePatternTM.GetType () == typeof ( LRCTM ) ) {
                ( (LRCTM) chasePatternTM ).LoadCenter ( _target );
            }
            if ( chasePatternTM.GetType() == typeof ( HSTM ) ) {
                ( (HSTM) chasePatternTM ).Bind ( _target );
            }
        }
        if ( chasePatternTM == guardPatternTM ) return;
        chasePatternTM.enabled = chasing;
        guardPatternTM.enabled = !chasing;
    }

    public override void Start () {
        chasing = false;
        base.Start ();
    }

    private void Update () {
        if ( ( transform.position - chaseAnchor.position ).magnitude > maxChaseRange ) {
            if ( chasing ) {
                PhaseChange ( false );
            }
        } else {
            return;
            /*
            if ( !chasing && agroRadar.collectedCount != 0 ) {
                PhaseChange ( true, agroRadar.collectedColliders [ 0 ].transform );
            }
            */
        }
    }

}
