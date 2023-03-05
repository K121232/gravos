using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoPlayerBridge : Zetha {

    public  Animator        deathPanel;
    public  TeflonPMove     movement;

    public  PowerCell       shieldCell;
    public  float           STRShield;

    public void GUIRESET () {
        SceneManager.LoadScene ( 0 );
    } 

    public override void DeltaIncoming ( int id, float delta ) {
        if ( CheckID ( id ) ) {
            
            delta *= hitboxes [ id ].beta;
            delta -= shieldCell.VariDrain ( delta * STRShield );
            currentHealth -= delta;

            if ( currentHealth <= 0 ) {
                deathPanel.SetBool ( "Dispatch", true );
                movement.acc = 0;
                movement.angleAcc = 0;
                movement.angleNeutralDrag = 0;
                movement.angleControlDrag = 0;
                for ( int i = 1; i < movement.transform.childCount; i++ ) {
                    movement.transform.GetChild ( i ).gameObject.SetActive ( false );
                }
            }
        }
    }
}
