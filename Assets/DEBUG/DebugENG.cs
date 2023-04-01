using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugENG : MonoBehaviour {
    public  PowerNode[]     na;
    public TMP_Text[]      ta;

    public  PowerCell[]   ca;
    public  Slider[]      sa;

    public  PowerCord[]   pca;
    public  Slider[]      pcsa;

    public bool             online = false;

    private void Start () {
        ta = new TMP_Text [ na.Length ];
        sa = new Slider [ ca.Length ];
        pcsa = new Slider [ pca.Length ];
        for ( int i = 0; i < na.Length; i++ ) {
            ta [ i ] = na [ i ].transform.GetChild ( 1 ).GetComponent<TMP_Text> ();
        }
        for ( int i = 0; i < ca.Length; i++ ) {
            sa [ i ] = ca [ i ].transform.GetChild ( 1 ).GetComponent<Slider> ();
        }
        for ( int i = 0; i < pca.Length; i++ ) {
            pcsa [ i ] = pca [ i ].transform.GetChild ( 1 ).GetComponent<Slider> ();
        }
        online = true;
        Debug.Log ( "!!!!!!" );
    }

    void LateUpdate () {
        if ( !online ) return;
        for ( int i = 0; i < na.Length; i++ ) {
           ta [ i ].text = ( na [ i ].load - na [ i ].magic1 - na [ i ].magic2 ).ToString ( "0.00" );
        }

        for ( int i = 0; i < sa.Length; i++ ) {
            sa [ i ].value = 100 * ca [ i ].resourceCurrent / ca [ i ].resourceMax;
        }

        for ( int i = 0; i < pca.Length; i++ ) {
            pcsa [ i ].value = pca [ i ].load;
        }
    }

    private void Update () {
        if ( Input.GetKeyDown ( KeyCode.B ) ) {
            na [ 0 ].TrimDrain ( -10 );
        }
        if ( Input.GetKeyDown ( KeyCode.N ) ) {
            na [ 0 ].TrimDrain ( 10 );
        }
    }
}
