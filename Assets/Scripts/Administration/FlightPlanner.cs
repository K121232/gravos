using UnityEngine;
[ExecuteInEditMode]
public class FlightPlanner : MonoBehaviour {
    public  Transform       handlesRoot;

    public  Vector2[]       points;
    public  float           resolution;

    public  float           scale = 1;

    public Vector2 Gitmas ( int a2, float a1 ) {
        float ma = 1 - a1;
        return points [ a2 ] * ma * ma * ma
                + 3 * (
                    ma * ma * a1 * ( points [ a2 ] + points [ a2 + 1 ]      * ( ( a2 / 2 ) % 2 == 0 ? 1 : -1 ) ) +
                    ma * a1 * a1 * ( points [ a2 + 2 ] + points [ a2 + 3 ]  * ( ( a2 / 2 ) % 2 == 0 ? 1 : -1 ) )
                )
                + a1 * a1 * a1 * points [ a2 + 2 ];
    }

    private void DrawCourse () {
        if ( points.Length <= 0 ) return;
        Vector2 delta = points [ 0 ], delta2;
        for ( int i = 0; i < points.Length - 3; i += 2 ) {
            if ( resolution <= 0 ) return;
            for ( float j = 0; j <= 1; j += resolution ) {
                delta2 = Gitmas ( i, j ) * scale;
                Debug.DrawLine ( delta, delta2, Color.white );
                delta = delta2;
            }
        }
    }

    void Update () {
        points = new Vector2 [ handlesRoot.childCount + 2 ];
        for ( int i = 0; i < handlesRoot.childCount + 2; i++ ) {
            points [ i ] = handlesRoot.GetChild ( ( i ) % handlesRoot.childCount ).position * scale;
            if ( i % 2 == 1 ) {
                Debug.DrawLine ( points [ i - 1 ], points [ i ], Color.red );
                points [ i ] -= points [ i - 1 ];
            }
        } 
        DrawCourse ();
    }
}
