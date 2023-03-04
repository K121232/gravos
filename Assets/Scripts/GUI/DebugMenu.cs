using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour {
    public  GameObject      mainPanel;
    private Animator        panelAnimation;

    public  TeflonPMove     playerShip;
    public  CameraTether    camTether;

    public  Slider[]        sliders;
    public  TMP_Text []      lables;

    public  TMP_Text        fbwStatus;
    private bool            deltaFBWS;

    private void Start() {
        sliders[0].SetValueWithoutNotify( playerShip.mxv );
        sliders[1].SetValueWithoutNotify( playerShip.acc );
        sliders[2].SetValueWithoutNotify( playerShip.angleMxv );
        sliders[3].SetValueWithoutNotify( playerShip.angleAcc );
        sliders[4].SetValueWithoutNotify( camTether.rotationStrength );
        panelAnimation = mainPanel.GetComponent<Animator> ();
        panelAnimation.SetBool ( "Dispatch", false );
        deltaFBWS = playerShip.flybywire;
        Redraw ();
    }
    
    public void Redraw () {
        for ( int i = 0; i < 5; i++ ) {
            lables[i].text = sliders[i].value.ToString( "0.00" );
        }
        fbwStatus.text = deltaFBWS ? "ON" : "OFF";
    }

    public void ToggleFWBS () {
        deltaFBWS = !deltaFBWS;
        Redraw ();
    }

    public void OnSaveMenu () {
        playerShip.mxv = sliders [ 0 ].value;
        playerShip.acc = sliders [ 1 ].value;
        playerShip.angleMxv = sliders [ 2 ].value;
        playerShip.angleAcc = sliders [ 3 ].value;
        camTether.rotationStrength = sliders [ 4 ].value;
        playerShip.flybywire = deltaFBWS;
        Redraw ();
        panelAnimation.SetBool ( "Dispatch", false );
    }

    public void Update() {
        if ( Input.GetKeyDown (KeyCode.M) ) {
            deltaFBWS = playerShip.flybywire;
            panelAnimation.SetBool ( "Dispatch", !panelAnimation.GetBool("Dispatch") );
        } 
    }
}
