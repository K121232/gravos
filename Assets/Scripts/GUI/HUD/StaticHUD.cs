using UnityEngine;
using UnityEngine.UI;

public class StaticHUD : MonoBehaviour {
    // Not needed yet, but should be included here nonetheless
    public  LabelNotification   ntp;    // Notification Panel

    public  float               aimZoomUpTime = 2;
    private float               aimZoomDeltaTime;

    public  AimHelper           aimZoomHelper;
    private float               aimZoomBias = 1;
    public  Slider              aimZoomSlider;
    public  Animator            aimZoomAnimator;

    void LateUpdate () {
        if ( aimZoomDeltaTime > 0 ) {
            aimZoomDeltaTime -= Time.deltaTime;
            if ( aimZoomDeltaTime < 0 ) {
                aimZoomAnimator.SetBool ( "HasChanged", false );
            }
        }
        if ( aimZoomBias != aimZoomHelper.GetZoomBias () ) {
            aimZoomBias = aimZoomHelper.GetZoomBias ();
            aimZoomAnimator.SetBool ( "HasChanged", true );
            aimZoomDeltaTime = aimZoomUpTime;
        }
        aimZoomSlider.value = aimZoomBias;
    }
}
