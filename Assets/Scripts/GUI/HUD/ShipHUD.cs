using UnityEngine.UI;
using UnityEngine;

public class ShipHUD : MonoBehaviour {
    public  ProtoPlayerBridge   bridge;
    public  Slider              hpSlider;

    public  PowerCell[]         cells;
    public  Slider[]            sliders;
    public  float[]             scales;

    public  ItemPickup          itemPickup;
    private bool                deltaIPS;

    public  LabelNotification   ntp;    // Notification Panel

    private void SetScale ( Transform sliderRoot, float aScale ) {
        sliderRoot.GetChild ( 1 ).GetChild ( 0 ).GetComponent<Image> ().pixelsPerUnitMultiplier = aScale * 0.44f;
    }

    private void Start () {
        SetScale ( hpSlider.transform, bridge.baseHealth );
        for ( int i = 0; i < cells.Length; i++ ) {
            SetScale ( sliders [ i ].transform, scales [ i ] );
        }
        ntp.AddMessage ( new DataLinkNTF ( "Press B to open the briefing menu", 5 ) );
        deltaIPS = false;
    }

    void LateUpdate () {
        hpSlider.value      = bridge.GetProcentHP () * hpSlider.maxValue;
        for ( int i = 0; i < cells.Length; i++ ) {
            sliders [ i ].value = cells [ i ].GetAvailableLoad () * sliders [ i ].maxValue;
            
            if ( cells [ i ].IsAvailable () ) {
                sliders [ i ].transform.GetChild ( 0 ).GetComponent<Image>().color = Color.white;
            } else {
                sliders [ i ].transform.GetChild ( 0 ).GetComponent<Image> ().color = Color.red;
            }
        }
        if ( deltaIPS != itemPickup.canPickup ) {
            deltaIPS = itemPickup.canPickup;
            if ( deltaIPS ) {
                ntp.AddMessage ( new DataLinkNTF ( "Press Q to collect the item", -1, 2 ) );
            } else {
                ntp.RemoveMessage ( "Press Q to collect the item" );
            }
        }
    }
}
