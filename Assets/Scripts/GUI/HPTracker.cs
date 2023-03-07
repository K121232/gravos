using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HPTracker : MonoBehaviour {
    public  Transform   player;

    public  RectTransform       dsp;
    public  Transform   target;

    public  BoxCollider2D   boundingBox;

    private Slider      hpSlider;
    private TMP_Text    label;

    public  string      displayName;

    public  Zetha       bridge;

    private void Start () {
        hpSlider    = dsp.GetChild (0).GetComponent<Slider> ();
        label       = dsp.GetChild (1).GetComponent<TMP_Text> ();
    }

    void Update () {
        if ( !target.gameObject.activeInHierarchy ) {
            dsp.gameObject.SetActive (false);
            return;
        }
        Vector2 delta = boundingBox.ClosestPoint ( player.position );

        dsp.position = delta;

        delta += ( delta -  (Vector2) target.position ) * 5;

        //dsp.rotation = Quaternion.Euler( 0, 0, 90 ) * target.rotation;

        label.text = displayName;
        hpSlider.value = bridge.GetProcentHP () * hpSlider.maxValue;
    }
}
