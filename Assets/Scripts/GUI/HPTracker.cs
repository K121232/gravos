using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HPTracker : MonoBehaviour {
    public  Transform   player;
    public  Transform   canvasRoot;

    [System.Serializable]
    public struct EnemyContact {
        public  Zetha           bridge;
        public  string          displayName;
        public  Collider2D      boundingBox;
        public  RectTransform   linked;
        [HideInInspector]
        public  Slider          slider;
    }
    public  EnemyContact[]      tracked;

    public  GameObject          HUDObject;

    private void SetScale ( Transform sliderRoot, float aScale ) {
        sliderRoot.GetChild ( 1 ).GetChild ( 0 ).GetComponent<Image> ().pixelsPerUnitMultiplier = aScale * 0.066f;
    }

    private void Start () {
        for ( int i = 0; i < tracked.Length; i++ ) {
            tracked [ i ].linked = Instantiate ( HUDObject, canvasRoot ).GetComponent<RectTransform>();
            tracked [ i ].slider = tracked [ i ].linked.GetChild ( 0 ).GetComponent<Slider> ();
            tracked [ i ].linked.GetChild ( 1 ).GetComponent<TMP_Text> ().text = tracked [ i ].displayName;
            SetScale ( tracked [ i ].slider.transform, tracked [ i ].bridge.baseHealth );
        }
    }

    void Update () {
        for ( int i = 0; i < tracked.Length; i++ ) {
            if ( !tracked [ i ].bridge.gameObject.activeInHierarchy ) {
                tracked [ i ].linked.gameObject.SetActive ( false );
                continue;
            }
            Vector2 delta = tracked [ i ].boundingBox.ClosestPoint ( player.position );

            tracked [ i ].linked.position = delta;

            delta += ( delta - (Vector2) tracked [ i ].bridge.transform.position ) * 5;

            //dsp.rotation = Quaternion.Euler( 0, 0, 90 ) * target.rotation;

            tracked [ i ].slider.value = tracked [ i ].bridge.GetProcentHP () * tracked [ i ].slider.maxValue;
        }
    }
}
