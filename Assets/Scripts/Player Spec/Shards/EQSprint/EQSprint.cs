using UnityEngine;

public class EQSprint : EQBase {
    [Header ( "EQ Sprint" )]

    public  float           sprintSTR;
    public  PowerCell       sprintCell;

    public  float           drainInit;
    public  float           drainCont;

    public  bool            isSprinting;

    public override void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        base.MainInit ( port );
        if ( enabled ) {
            sprintCell = port.batteryLink.GetChild ( 2 ).GetComponent<PowerCell> ();
        } else {
            sprintCell = null;
        }
    }

    void Update () {
        if ( Input.GetAxis ( "Fire2" ) > 0 && sprintCell.Available() ) {
            if ( !isSprinting ) {
                if ( sprintCell.VariDrain ( drainInit ) == drainInit ) {
                    isSprinting = true;
                }
            }
        } else {
            isSprinting = false;
        }
        if ( isSprinting ) {
            float contDrainByTime = drainCont * Time.deltaTime;
            if ( sprintCell.VariDrain ( contDrainByTime ) == contDrainByTime ) {
                rgb.AddForce ( transform.up * sprintSTR );
            } else {
                isSprinting = false;
            }
        }
    }
}
