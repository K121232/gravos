using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProtoPlayerBridge : Zetha {

    public  Animator        deathPanel;
    public  TMP_Text        deathLabel;
    public  TeflonPMove     movement;

    public  PowerCell       shieldCell;
    public  float           STRShield;

    public void GUIRESET () {
        SceneManager.LoadScene ( 0 );
    }

    public  float   stunTime;
    private float   deltaTime;

    private void Update () {
        if ( deltaTime > 0 && currentHealth > 0 ) {
            deltaTime -= Time.deltaTime;
            if ( deltaTime <= 0 ) {
                deltaTime = 0;
                movement.SetLock (false);
            }
        }    
    }

    public override void DeltaIncoming ( int id, float delta ) {
        if ( CheckID ( id ) ) {
            
            delta *= hitboxes [ id ].beta;
            delta -= shieldCell.VariDrain ( delta * STRShield ) / STRShield;
            currentHealth -= delta;
            movement.SetLock ( true );
            deltaTime = stunTime;

            if ( currentHealth <= 0.01f ) {
                Detach ( "GAME OVER" );
            }
        }
    }

    public  void    Detach  ( string messig ) {
        deathPanel.SetBool ( "Dispatch", true );
        deathLabel.text = messig;
        movement.SetLock ( true );
        for ( int i = 1; i < movement.transform.childCount; i++ ) {
            movement.transform.GetChild ( i ).gameObject.SetActive ( false );
        }
    }

    public void Reattach () {
        deathPanel.SetBool ( "Dispatch", false );

        movement.SetLock ( false );

        for ( int i = 1; i < movement.transform.childCount; i++ ) {
            movement.transform.GetChild ( i ).gameObject.SetActive ( true );
        }
    }
}
