using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MenuCore {
    public  TeflonPMove     playerShip;
    public  CarrotTargeting carrotTargeting;
    public  CameraTether    camTether;

    public  Slider[]        sliders;
    public  TMP_Text []      lables;

    public  TMP_Text        fbwStatus;
    private bool            deltaFBWS;

    public  TMP_Text        maStatus;
    private bool            deltaMAS;

    public override void Start() {
        base.Start ();
        sliders [ 0].SetValueWithoutNotify( playerShip.mxv );
        sliders[1].SetValueWithoutNotify( playerShip.acc );
        sliders[2].SetValueWithoutNotify( playerShip.angleMxv );
        sliders[3].SetValueWithoutNotify( playerShip.angleAcc );
        sliders[4].SetValueWithoutNotify( camTether.rotationStrength );
        deltaFBWS   = playerShip.flybywire;
        deltaMAS    = carrotTargeting.enabled;
        Redraw ();
    }
    
    public void Redraw () {
        for ( int i = 0; i < 5; i++ ) {
            lables[i].text = sliders[i].value.ToString( "0.00" );
        }
        fbwStatus.text  = deltaFBWS ? "ON" : "OFF";
        maStatus.text   = deltaMAS  ? "ON" : "OFF";
    }

    public void Toggle ( int a ) {
        if ( a == 0 ) {
            deltaFBWS = !deltaFBWS;
        } 
        if ( a == 1 ) {
            deltaMAS = !deltaMAS;
        }
        Redraw ();
    }

    public void OnSaveMenu () {
        playerShip.mxv = sliders [ 0 ].value;
        playerShip.acc = sliders [ 1 ].value;
        playerShip.angleMxv = sliders [ 2 ].value;
        playerShip.angleAcc = sliders [ 3 ].value;
        camTether.rotationStrength = sliders [ 4 ].value;

        playerShip.flybywire = deltaFBWS;
        carrotTargeting.enabled = deltaMAS;

        Redraw ();
        Backflow ( false );
    }

    public void Update() {
        if ( Input.GetKeyDown (KeyCode.M) ) {
            deltaFBWS   = playerShip.flybywire;
            deltaMAS    = carrotTargeting.enabled;
            Backflow ( true );
        } 
    }
}
