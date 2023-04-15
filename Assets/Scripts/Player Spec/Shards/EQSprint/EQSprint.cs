using UnityEngine;

public class EQSprint : EQBase {
    [Header ( "EQ Sprint" )]
    public  float           sprintSTR;

    public override void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        base.MainInit ( port );
        if ( enabled ) {
            cell = port.batteryLink.GetChild ( 2 ).GetComponent<PowerCell> ();
        } else {
            cell = null;
        }
    }

    public override GameObject Fire () {
        rgb.AddForce ( transform.up * sprintSTR );
        return base.Fire ();
    }
}
