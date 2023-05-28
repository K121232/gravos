using UnityEngine;
using TMPro;

public class Ododek : MonoBehaviour {
    public  Radar []        bodies;
    public  bool[]          actifity;

    public  Thunder         thunder;

    public  string          raport;
    private int             totalShots, hitShots;
    private float           totalTime, timeOnTarget;

    public  bool            resetP;

    public TMP_Text         label;
    
    public  void ToggleBodies ( int which ) {
        actifity [ which ] = !actifity [ which ];
    }

    public  void ResetShots () {
        totalShots = 0;
        hitShots = 0;
        totalTime = 0;
        timeOnTarget = 0;
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
        if ( thunder.GetTFCP () > 0.99f ) {
            timeOnTarget += Time.deltaTime;
        }
        totalTime += Time.deltaTime;
        if ( totalShots > 0 ) {
            raport = ( 100 * (float) hitShots / totalShots ).ToString("0.00");
        } else {
            raport = "N/A";
        }
        if ( timeOnTarget > 0 ) {
            raport += "\n" + ( 100 * ( float ) timeOnTarget / totalTime ).ToString ( "0.00" );
        } else {
            raport += " \nN/A";
        }
        raport += "\n" + ( thunder.target != null ? thunder.target.name : "N/A" );
        label.text = raport;
    }
}
