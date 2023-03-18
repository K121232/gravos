using UnityEngine;
using UnityEngine.UI;

public class ShipHUD : MonoBehaviour {
    public  RectTransform       tRoot;
    public  ProtoPlayerBridge   bridge;
    public  Slider              hpSlider;

    public  PowerCell           shieldCell;
    public  Slider              shieldSlider;

    public  PowerCell           corvoCell;
    public  Slider              timeSlider;

    public  float               STRV;
    public  float               STRV2;
    private Vector2             pastV;
    private Rigidbody2D         rgb;

    private void SetScale ( Transform sliderRoot, float aScale ) {
        sliderRoot.GetChild ( 1 ).GetChild ( 0 ).GetComponent<Image> ().pixelsPerUnitMultiplier = aScale * 0.44f;
    }

    private void Start () {
        hpSlider.transform.rotation = Quaternion.Euler ( 0, 0, Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
        shieldSlider.transform.rotation = Quaternion.Euler ( 0, 0, Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
        timeSlider.transform.rotation = Quaternion.Euler ( 0, 0, 180 - Mathf.Atan2 ( Screen.height, Screen.width ) * Mathf.Rad2Deg );
        SetScale ( hpSlider.transform, bridge.baseHealth );
        SetScale ( shieldSlider.transform, shieldCell.resourceMax / bridge.STRShield );
        SetScale ( timeSlider.transform, corvoCell.resourceMax );

        rgb = bridge.GetComponent<Rigidbody2D> ();
    }

    void LateUpdate () {
        hpSlider.value      = bridge.GetProcentHP () * hpSlider.maxValue;
        shieldSlider.value  = shieldCell.GetAvailableLoad () * shieldSlider.maxValue;
        timeSlider.value    = corvoCell.GetAvailableLoad () * timeSlider.maxValue;
        tRoot.rotation = Quaternion.FromToRotation ( (Vector3) rgb.velocity - (Vector3) pastV + new Vector3 ( 0, 0, STRV ), Vector3.forward );
        pastV = Vector2.Lerp ( pastV, rgb.velocity, STRV2 * Time.deltaTime );
    }
}
