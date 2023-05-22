using UnityEngine;

public class WarningDisplay : MonoBehaviour {
    public  Radar       foeRadar;

    public  Transform   warningRoot;
    public  GameObject  warningPrefab;
    public  RectTransform[]   warningHandles;
    public  float       warningCircleRange;

    public  float       warningActivationThreshold;

    private int         mxw = 10;

    private void Start () {
        warningHandles = new RectTransform [ mxw ];
        for ( int i = 0; i < mxw; i++ ) {
            warningHandles [ i ] = Instantiate ( warningPrefab, warningRoot ).GetComponent<RectTransform>();
        }
    }

    void LateUpdate () {
        int progress = 0;
        if ( foeRadar.collectedCount != 0 ) {
            for ( int i = 0; i < foeRadar.collectedCount; i++ ) {
                Transform deltaHPR = foeRadar.collectedColliders [ i ].transform.parent.parent.GetChild(2);
                Zeus[]   list = deltaHPR.GetComponents<Zeus>();
                for ( int j = 0; j < list.Length; j++ ) {
                    if ( list [ j ].IsTracking () && list [ j ].launchIsDetectable ) {
                        for ( int k = 0; k < list [ j ].turrets.Length; k++ ) {
                            float urubega = list [ j ].turrets [ k ].GetFireProgress ();
                            if ( urubega >= warningActivationThreshold ) {
                                if ( progress < mxw ) {
                                    warningHandles [ progress ].gameObject.SetActive ( true );
                                    Vector3 delta = transform.position + ( list [ j ].turrets [ k ].transform.position - transform.position ).normalized * warningCircleRange;
                                    warningHandles [ progress ].position = delta;
                                    for ( int n = 0; n < 3; n++ ) {
                                        warningHandles [ progress ].GetComponent<Multihelper> ().anchors [ 1 + n ].gameObject.SetActive ( ! ( ( 1 - warningActivationThreshold ) * n / 3 >= urubega - warningActivationThreshold ) );
                                    }
                                    progress++;
                                }
                            }
                        }
                    }
                }
            }
        }
        for ( int i = progress; i < mxw; i++ ) {
            warningHandles [ i ].gameObject.SetActive ( false );
        }
    }
}
