using UnityEngine;

public class LRCTM : TM {
    public  Vector2[]     points;
    public  float           resolution;

    public  float         scanDistance;

    public  int             major = 0;
    public  float           minor = 0;

    public  Vector2 Gitmas ( int a2, float a1 ) {
        return points [ a2 ] * ( 1 - a1 ) * ( 1 - a1 ) * ( 1 - a1 ) + 3 * ( ( 1 - a1 ) * ( 1 - a1 ) * a1 * ( points [ a2 ] + points [ a2 + 1 ] * ( a2 % 4 == 0 ? 1 : -1 ) ) + ( 1 - a1 ) * a1 * a1 * ( points [ a2 + 2 ] + points [ a2 + 3 ] ) ) + a1 * a1 * a1 * points [ a2 + 2 ];
    }

    public override void Update () {
        float minA = 180;
        float delta3 = 0;

        float   deltaMIN = minor;
        int     deltaMAJ = major;

        Vector2 delta = points [ 0 ], delta2;
        for ( int i = 0; i < points.Length - 3; i += 2 ) {
            if ( resolution <= 0 ) return;
            for ( float j = 0; j <= 1; j += resolution ) {
                delta2 = Gitmas ( i, j );

                Debug.DrawLine ( delta, delta2 );

                if ( Mathf.Abs ( ( major - i + points.Length - 4 ) % ( points.Length - 4 ) + minor - j ) < scanDistance ) {
                    delta3 = Vector2.Angle ( delta - delta2, (Vector2) transform.position - delta2 );
                    if ( j != 0 && delta3 < minA ) {
                        minA = delta3;
                        deltaMAJ = i;
                        deltaMIN = j;
                    }
                }
                major = deltaMAJ;
                minor = deltaMIN;
                delta = delta2;
            }
        }
        Debug.DrawLine ( transform.position, Gitmas ( major, minor ), Color.red );
        targetLink = Gitmas ( major, minor );
        base.Update ();
    }
}
