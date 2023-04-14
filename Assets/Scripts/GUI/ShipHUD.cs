using UnityEngine;
using UnityEngine.UI;

public class ShipHUD : MonoBehaviour {
    public  ProtoPlayerBridge   bridge;
    public  Slider              hpSlider;

    public  PowerCell           shieldCell;
    public  Slider              shieldSlider;

    public  PowerCell           corvoCell;
    public  Slider              timeSlider;

    private void SetScale ( Transform sliderRoot, float aScale ) {
        sliderRoot.GetChild ( 1 ).GetChild ( 0 ).GetComponent<Image> ().pixelsPerUnitMultiplier = aScale * 0.44f;
    }

    private void Start () {
        SetScale ( hpSlider.transform, bridge.baseHealth );
        SetScale ( shieldSlider.transform, shieldCell.resourceMax / bridge.STRShield );
        SetScale ( timeSlider.transform, corvoCell.resourceMax );
    }

    void LateUpdate () {
        hpSlider.value      = bridge.GetProcentHP () * hpSlider.maxValue;
        shieldSlider.value  = shieldCell.GetAvailableLoad () * shieldSlider.maxValue;
        timeSlider.value    = corvoCell.GetAvailableLoad () * timeSlider.maxValue;
    }
}
