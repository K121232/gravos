using UnityEngine;

public class EQSprint : MonoBehaviour {
    public  Rigidbody2D     rgb;

    public  float           sprintSTR;
    public  PowerCell       sprintCell;

    public  float           drainInit;
    public  float           drainCont;

    public  bool            isSprinting;

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
