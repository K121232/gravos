using UnityEngine;
using UnityEngine.UI;

public class ShipHUD : MonoBehaviour {
    public  ProtoPlayerBridge   bridge;
    public  Slider              hpSlider;

    public  PowerCell           shieldCell;
    public  Slider              shieldSlider;

    private void SetScale ( Transform sliderRoot, float aScale ) {
        sliderRoot.GetChild ( 1 ).GetChild ( 0 ).GetComponent<Image> ().pixelsPerUnitMultiplier = aScale * 0.66f;
    }

    private void Start () {
        hpSlider.transform.rotation = Quaternion.Euler ( 0, 0, Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
        shieldSlider.transform.rotation = Quaternion.Euler ( 0, 0, Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
        SetScale ( hpSlider.transform, bridge.baseHealth );
        SetScale ( shieldSlider.transform, shieldCell.resourceMax / bridge.STRShield );
    }

    void LateUpdate () {
        hpSlider.value = bridge.GetProcentHP () * hpSlider.maxValue;
        shieldSlider.value = shieldCell.GetAvailableLoad () * shieldSlider.maxValue;
    }
}
