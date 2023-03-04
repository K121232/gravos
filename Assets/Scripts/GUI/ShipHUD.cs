using UnityEngine;
using UnityEngine.UI;

public class ShipHUD : MonoBehaviour {
    public  ProtoPlayerBridge   bridge;
    public  Slider              hpSlider;

    private void Start () {
        hpSlider.transform.rotation = Quaternion.Euler ( 0, 0, Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
    }

    void LateUpdate () {
        hpSlider.value = ( bridge.currentHealth / bridge.baseHealth ) * hpSlider.maxValue;
    }
}
