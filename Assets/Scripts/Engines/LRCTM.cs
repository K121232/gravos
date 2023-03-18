using UnityEngine;

public class LRCTM : TM {
    public  Vector2[]     points;
    public  float           resolution;

    public  float           scanDistance;

    public  int             major = 0;
    public  float           minor = 0;

    public  float           STRV1;       // Pathfind velocity offset
    public  float           STRV2;      // Targeting velocity offset
    public  float           STRVT;      // Targeting velocity offset

    public  Transform       center;
    private Rigidbody2D     centerRGB;
    private Vector2         offset = new Vector2 ( 0, 0 );

    public  Vector2 Gitmas ( int a2, float a1 ) {
        return offset + points [ a2 ] * ( 1 - a1 ) * ( 1 - a1 ) * ( 1 - a1 ) + 3 * ( ( 1 - a1 ) * ( 1 - a1 ) * a1 * ( points [ a2 ] + points [ a2 + 1 ] * ( a2 % 4 == 0 ? 1 : -1 ) ) + ( 1 - a1 ) * a1 * a1 * ( points [ a2 + 2 ] + points [ a2 + 3 ] ) ) + a1 * a1 * a1 * points [ a2 + 2 ];
    }

    private void DrawCourse () {
        Vector2 delta = points [ 0 ], delta2;
        for ( int i = 0; i < points.Length - 3; i += 2 ) {
            if ( resolution <= 0 ) return;
            for ( float j = 0; j <= 1; j += resolution ) {
                delta2 = Gitmas ( i, j );
                Debug.DrawLine ( delta, delta2 );
                delta = delta2;
            }
        }
    }

    public  void LoadCenter ( Transform a ) {
        center = a;
        centerRGB = center.GetComponent<Rigidbody2D> ();
    }

    public override void Update () {
        DrawCourse ();

        float   bestScore = Mathf.Infinity;
        Vector2 deltaA, deltaB = new Vector2 ( major, minor );
        Vector2 target = transform.position + (Vector3)rgb.velocity * STRV1;

        Vector2 DEBUGP = Vector2.zero;

        if ( scanDistance == 0 ) return;
        if ( center != null ) offset = center.position;
        if ( centerRGB != null ) offset -= centerRGB.velocity * STRV2;

        for ( float i = 0; i < scanDistance; i += resolution ) {
            int     md = ( major + Mathf.FloorToInt ( i + minor ) * 2 ) % ( points.Length - 2 );
            float   mi = minor + i - Mathf.Floor ( minor + i );
            deltaA = Gitmas ( md, mi );
            if ( ( target - deltaA ).sqrMagnitude < bestScore ) {
                bestScore = ( target - deltaA ).sqrMagnitude;
                deltaB = new Vector2 ( md, mi );
            } 
            if ( i != 0 ) {
                                                                Debug.DrawLine ( DEBUGP, deltaA, Color.magenta );
            }
            DEBUGP = deltaA;
        }

        major = Mathf.RoundToInt ( deltaB.x );
        minor = deltaB.y;

        targetLink = Gitmas ( major, minor ) - rgb.velocity * STRVT;

                                                                Debug.DrawLine ( target, targetLink, Color.red );

        base.Update ();
    }
}
