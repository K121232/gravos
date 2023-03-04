using UnityEngine;

public class IBR : MonoBehaviour {
    public  Transform       actualRoot;

    public  Radar           radar;
    private Transform[]     tracked;
    private GameObject[]    indicators;

    public  float           STRMAG;

    public  GameObject      band;
    private int             CBC;
    private int             PBC;        // past band count

    private void Start() {
        indicators = new GameObject[16];
        tracked = new Transform[16];
    }

    void LateUpdate() {
        CBC = Mathf.Min ( radar.collectedCount, 15 );
        for ( int i = 0; i < CBC; i++ ) {
            tracked[i] = radar.collectedColliders[i].transform;
        }
        if ( PBC != CBC ) {
            for ( int i = 0; i < CBC; i++ ) {
                if ( indicators[i] == null ) {
                    indicators[i] = Instantiate( band );
                    indicators[i].transform.SetParent( actualRoot );
                    indicators[i].transform.localPosition = Vector3.zero;
                }
                indicators[i].SetActive( true );
            }
            for ( int i = CBC; i < PBC; i++ ) {
                indicators[i].SetActive( false );
            }
            PBC = CBC;
        }
        for ( int i = 0; i < CBC; i++ ) {
            Vector2 delta = actualRoot.position - tracked[ i ].position;
            indicators[i].transform.rotation = Quaternion.Euler ( 0, 0, Vector2.SignedAngle ( Vector2.up, delta ) );
            indicators[i].GetComponent<SpriteRenderer>().material.SetFloat( "_D2", Mathf.Clamp ( 0.9f + delta.magnitude * STRMAG, 0.9f, 1f ) );
        }
    }
}
