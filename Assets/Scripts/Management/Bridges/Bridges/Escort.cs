using UnityEngine;

public class Escort : Zetha {
    private LRCTM       targetingModule;
    public  Transform   protectTarget;

    public  Radar       agroRadar;
    public  float       maxChaseRange;

    public  bool        chasing;

    public override void Start () {
        targetingModule = GetComponent<LRCTM> ();
        base.Start ();
    }

    private void Update () {
        if ( !chasing && agroRadar.collectedCount != 0 ) {
            chasing = true;
            targetingModule.LoadCenter ( agroRadar.collectedColliders [ 0 ].transform );
        }
        if ( chasing && ( transform.position - protectTarget.position ).magnitude > maxChaseRange ) {
            chasing = false;
            targetingModule.LoadCenter ( protectTarget );
        }
    }

}
