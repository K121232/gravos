using UnityEngine;

public class Karnaka : MonoBehaviour {
    public  Transform   mainHull;

    public  float       timelag;
    public  float       spacing;

    private Vector2[]   positions;
    private int         deltaPI;
    private int         pml;

    [System.Serializable]
    public  struct TPair {
        public Transform   beta1;
        public Transform   beta2;
    }

    public  TPair[]   spines;

    private int ADPI ( int a ) {
        return ( a + 1 ) % pml;
    }

    private void Start () {
        pml = Mathf.CeilToInt( timelag / 0.05f );
        Debug.Log ( pml );
        positions = new Vector2 [ pml ];
        for ( int i = 0; i < pml; i++ ) {
            positions [ i ] = mainHull.position;
        }
        deltaPI = 0;
    }

    private void LateUpdate () {
        for ( int i = 0; i < spines.Length; i++ ) {
            spines [ i ].beta1.position = Vector3.Lerp ( spines [ i ].beta1.position, positions [ ADPI ( Mathf.CeilToInt ( deltaPI + i * spacing ) ) ], 1 );
        }
        positions [ deltaPI ] = mainHull.position;
        deltaPI = ADPI ( deltaPI );
    }
}
