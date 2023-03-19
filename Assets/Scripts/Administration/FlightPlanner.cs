using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class FlightPlanner : MonoBehaviour {
    public  Transform       handlesRoot;

    public  Vector2[]       output;
    public  float           resolution;

    public  float           scale = 1;

    public Vector2 Gitmas ( int a2, float a1 ) {
        float ma = 1 - a1;
        return output [ a2 ] * ma * ma * ma
                + 3 * (
                    ma * ma * a1 * ( output [ a2 ] + output [ a2 + 1 ]      * ( ( a2 / 2 ) % 2 == 0 ? 1 : -1 ) ) +
                    ma * a1 * a1 * ( output [ a2 + 2 ] + output [ a2 + 3 ]  * ( ( a2 / 2 ) % 2 == 0 ? 1 : -1 ) )
                )
                + a1 * a1 * a1 * output [ a2 + 2 ];
    }

    private void DrawCourse () {
        if ( output.Length <= 0 ) return;
        Vector2 delta = output [ 0 ], delta2;
        for ( int i = 0; i < output.Length - 3; i += 2 ) {
            if ( resolution <= 0 ) return;
            for ( float j = 0; j <= 1; j += resolution ) {
                delta2 = Gitmas ( i, j ) * scale;
                Debug.DrawLine ( delta, delta2, Color.white );
                delta = delta2;
            }
        }
    }

    void Update () {
        output = new Vector2 [ handlesRoot.childCount + 2 ];
        for ( int i = 0; i < handlesRoot.childCount + 2; i++ ) {
            output [ i ] = handlesRoot.GetChild ( ( i ) % handlesRoot.childCount ).position * scale;
            if ( i % 2 == 1 ) {
                Debug.DrawLine ( output [ i - 1 ], output [ i ], Color.red );
                output [ i ] -= output [ i - 1 ];
            }
        } 
        DrawCourse ();
    }
}
