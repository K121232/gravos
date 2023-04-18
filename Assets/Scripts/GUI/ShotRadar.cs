using UnityEngine;

public class ShotRadar : MonoBehaviour {
    public  Radar       foeRadar;

    public  Transform   warningRoot;
    public  GameObject  warningPrefab;
    public  float       warningCircleRange;

    public  float       warningActivationThreshold;

    private int         mxw = 10;

    private void Start () {
        for ( int i = 0; i < mxw; i++ ) {
            Instantiate ( warningPrefab, warningRoot );
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
                                    warningRoot.GetChild ( progress ).gameObject.SetActive ( true );
                                    Vector3 delta = transform.position + ( list [ j ].turrets [ k ].transform.position - transform.position ).normalized * warningCircleRange;
                                    warningRoot.GetChild ( progress ).GetComponent<RectTransform> ().position = delta;
                                    progress++;
                                }
                            }
                        }
                    }
                }
            }
        }
        for ( int i = progress; i < mxw; i++ ) {
            warningRoot.GetChild ( i ).gameObject.SetActive ( false );
        }
    }
}
