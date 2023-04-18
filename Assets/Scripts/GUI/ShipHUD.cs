using UnityEngine;
using UnityEngine.UI;

public class ShipHUD : MonoBehaviour {
    public  ProtoPlayerBridge   bridge;
    public  Slider              hpSlider;

    public  PowerCell[]         cells;
    public  Slider[]            sliders;
    public  float[]             scales;

    private void SetScale ( Transform sliderRoot, float aScale ) {
        sliderRoot.GetChild ( 1 ).GetChild ( 0 ).GetComponent<Image> ().pixelsPerUnitMultiplier = aScale * 0.44f;
    }

    private void Start () {
        SetScale ( hpSlider.transform, bridge.baseHealth );
        for ( int i = 0; i < cells.Length; i++ ) {
            SetScale ( sliders [ i ].transform, scales [ i ] );
        }
    }

    void LateUpdate () {
        hpSlider.value      = bridge.GetProcentHP () * hpSlider.maxValue;
        for ( int i = 0; i < cells.Length; i++ ) {
            sliders [ i ].value = cells [ i ].GetAvailableLoad () * sliders [ i ].maxValue;
        }
    }
}
