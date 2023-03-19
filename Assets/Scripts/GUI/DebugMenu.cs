using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MenuCore {
    public  ProtoPlayerBridge   playerBridge;
    public  TeflonPMove         playerShip;
    public  CarrotTargeting     carrotTargeting;
    public  CameraTether        camTether;

    public  Slider[]            sliders;
    public  TMP_Text []         lables;

    [System.Serializable]
    public struct ToggleButton {
        public  TMP_Text        statusLabel;
        private bool            status;
        public void Toggle () {
            Set ( !status );
        }
        public void Set ( bool _status ) {
            status = _status;
            Redraw ();
        }
        public void Redraw () {
            statusLabel.text = status ? "ON" : "OFF";
        }
        public bool GetStatus () {
            return status;
        }
    }

    public  ToggleButton    FBW;
    public  ToggleButton    MAS;
    public  ToggleButton    INV;

    public override void Start () {
        base.Start ();
        sliders [ 0 ].SetValueWithoutNotify ( playerShip.mxv );
        sliders [ 1 ].SetValueWithoutNotify ( playerShip.acc );
        sliders [ 2 ].SetValueWithoutNotify ( playerShip.angleMxv );
        sliders [ 3 ].SetValueWithoutNotify ( playerShip.angleAcc );
        sliders [ 4 ].SetValueWithoutNotify ( camTether.rotationStrength );

        FBW.Set ( playerShip.flybywire );
        MAS.Set ( carrotTargeting.enabled );
        INV.Set ( playerBridge.invulnerability );

        Redraw ();
    }

    public void Redraw () {
        for ( int i = 0; i < 5; i++ ) {
            lables [ i ].text = sliders [ i ].value.ToString ( "0.00" );
        }
        FBW.Redraw ();
        MAS.Redraw ();
        INV.Redraw ();
    }

    public void Toggle ( int a ) {
        switch ( a ) {
            case 0:
                FBW.Toggle ();
                break;
            case 1:
                MAS.Toggle ();
                break;
            case 2:
                INV.Toggle ();
                break;
        }
        Redraw ();
    }

    public void OnSaveMenu () {
        playerShip.mxv = sliders [ 0 ].value;
        playerShip.acc = sliders [ 1 ].value;
        playerShip.angleMxv = sliders [ 2 ].value;
        playerShip.angleAcc = sliders [ 3 ].value;
        camTether.rotationStrength = sliders [ 4 ].value;

        playerBridge.invulnerability = INV.GetStatus ();
        playerShip.flybywire = FBW.GetStatus ();
        carrotTargeting.enabled = MAS.GetStatus ();

        Redraw ();
        Backflow ( false );
    }

    public void Update () {
        if ( Input.GetKeyDown ( KeyCode.M ) ) {

            FBW.Set ( playerShip.flybywire );
            MAS.Set ( carrotTargeting.enabled );
            INV.Set ( playerBridge.invulnerability );

            Backflow ( true );
        }
    }
}
