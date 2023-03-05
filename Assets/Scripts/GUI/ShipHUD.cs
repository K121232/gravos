using UnityEngine;
using UnityEngine.UI;

public class ShipHUD : MonoBehaviour {
    public  ProtoPlayerBridge   bridge;
    public  Slider              hpSlider;

    public  PowerCell           shieldCell;
    public  Slider              shieldSlider;

    private void Start () {
        hpSlider.transform.rotation = Quaternion.Euler ( 0, 0, Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
        shieldSlider.transform.rotation = Quaternion.Euler ( 0, 0, Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
    }

    void LateUpdate () {
        hpSlider.value      = ( bridge.currentHealth / bridge.baseHealth )              * hpSlider.maxValue;
        shieldSlider.value  = shieldCell.GetAvailableLoad()                             * shieldSlider.maxValue;
    }
}
