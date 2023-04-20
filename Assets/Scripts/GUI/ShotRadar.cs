using UnityEngine;

public class ShotRadar : MonoBehaviour {
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
                FireControlSystem[]   list = deltaHPR.GetComponents<FireControlSystem>();
                for ( int j = 0; j < list.Length; j++ ) {
                    if ( list [ j ].IsTracking () ) {
                        for ( int k = 0; k < list [ j ].turrets.Length; k++ ) {
                            if ( list [ j ].turrets [ k ].GetFiringProgress () >= warningActivationThreshold ) {
                                if ( progress < mxw ) {
                                    warningHandles[ progress ].gameObject.SetActive ( true );
                                    Vector3 delta = transform.position + ( list [ j ].turrets [ k ].transform.position - transform.position ).normalized * warningCircleRange;
                                    warningHandles [ progress ].position = delta;
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
