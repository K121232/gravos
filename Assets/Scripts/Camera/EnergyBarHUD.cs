using UnityEngine;
using UnityEngine.UI;

public class EnergyBarHUD : MonoBehaviour {
    public  Image           target;
    public  PowerCell    energySystem;

    void LateUpdate() {
        RectTransform rt = target.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2( 2 * energySystem.resourceCurrent, 15 );
    }
}
