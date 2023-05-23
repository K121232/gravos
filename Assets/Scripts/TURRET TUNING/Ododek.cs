using UnityEngine;

public class Ododek : MonoBehaviour {
    public  Radar []        bodies;
    public  bool[]          actifity;

    public  Thunder         thunder;

    public  string          percentage;
    private int             totalShots, hitShots;

    public  bool            resetP;

    private void ResetShots () {
        totalShots = 0;
        hitShots = 0;
    }

    void Start () {
        ResetShots ();
    }

    void Update () {
        for ( int i = 0; i < actifity.Length; i++ ) {
            bodies [ i ].transform.parent.gameObject.SetActive ( actifity [ i ] );
        }
        for ( int i = 0; i < bodies.Length; i++ ) {
            if ( bodies [ i ].collectedCount != 0 ) {
                hitShots++;
                bodies [ i ].collectedColliders [ 0 ].gameObject.SetActive ( false );
            }
        }
        if ( resetP ) {
            resetP = false;
            ResetShots ();
        }
        if ( thunder.IsFiring() ) {
            totalShots++;
        }
        if ( totalShots > 0 ) {
            percentage = ( 100 * (float) hitShots / totalShots ).ToString("0.00f");
        } else {
            percentage = "NOT ANY";
        }
    }
}
