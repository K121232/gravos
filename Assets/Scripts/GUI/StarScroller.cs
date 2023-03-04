using UnityEngine;

public class StarScroller : MonoBehaviour {
    [System.Serializable]
    public class StarEntry {
        public  Material    mat;
        public  float   strPOS;
        public  float   strVEL;
        [HideInInspector]
        public  Vector2 delta;  // old manual offset
    }
    public StarEntry[]    starLayers;

    void LateUpdate() {

        for ( int i = 0; i < starLayers.Length; i++ ) {
            starLayers [ i ].delta = Vector2.Lerp ( starLayers [ i ].delta, transform.position * starLayers[i].strPOS, starLayers [ i ].strVEL * Time.deltaTime );
            starLayers[ i ].mat.SetVector( "_ManualOffset", starLayers [ i ].delta );
        }
    }
}
