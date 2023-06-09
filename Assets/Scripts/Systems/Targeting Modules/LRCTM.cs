using UnityEngine;

public class LRCTM : TM {
    public  Vector2[]     positionPoints;
    public  Vector2[]     controlPoints;

    public  float           stepsize;

    public  float           searchRange;

    public  float           currentPos;

    public  bool            bidirectional;

    public Vector2 PathGeneratorFunction ( float alpha ) {
        int a1      = Mathf.FloorToInt ( alpha );
        float a2    = alpha - a1;
        int b1 = ( a1 + 1 ) % controlPoints.Length;
        float ma = 1 - a2;
        return positionPoints [ a1 ] * ma * ma * ma
                + 3 * (
                    ma * ma * a2 * ( positionPoints [ a1 ] + controlPoints [ a1 ] * ( a1 % 2 == 0 ? 1 : -1 ) ) +
                    ma * a2 * a2 * ( positionPoints [ b1 ] + controlPoints [ b1 ] * ( a1 % 2 == 0 ? 1 : -1 ) )
                )
                + a2 * a2 * a2 * positionPoints [ b1 ];
    }

    private void DrawCourse () {
        Vector2 delta = PathGeneratorFunction ( 0 ), delta2;
        if ( stepsize <= 0 ) return;
        for ( float i = 0; i < controlPoints.Length; i += stepsize * 1 ) {
            delta2 = PathGeneratorFunction ( i );
            Debug.DrawLine ( delta, delta2 );
            delta = delta2;
        }
    }

    protected float SafeProgress ( float alpha ) {
        alpha += controlPoints.Length;
        return alpha - Mathf.FloorToInt ( alpha / controlPoints.Length ) * controlPoints.Length;
    }

    public override void Update () {
        if ( controlPoints.Length == 0 ) { enabled = false; return; }
        DrawCourse ();

        float bestScore = Mathf.Infinity;
        float deltaScore;

        Vector2 deltaPastPoint = PathGeneratorFunction ( SafeProgress ( currentPos - ( bidirectional ? searchRange : 0 ) - stepsize ) );
        Vector2 deltaPoint;

        for ( float i = currentPos - ( bidirectional ? searchRange : 0 ); i < currentPos + searchRange; i += stepsize ) {
            deltaPoint = PathGeneratorFunction ( SafeProgress ( i ) );

            //Debug.DrawLine ( deltaPoint, deltaPoint + ( deltaPastPoint - deltaPoint ).normalized * 50, Color.red );
            
            // Invert the delta and past point to make sure both the large and small angle are considered when intersecting two lines

            deltaScore = Vector2.Angle ( ( Vector3 ) deltaPoint - transform.position, deltaPastPoint - deltaPoint );

            if ( deltaScore < bestScore ) {
                bestScore = deltaScore;
                currentPos = SafeProgress ( i );
            }

            deltaScore = Vector2.Angle ( ( Vector3 ) deltaPoint - transform.position, deltaPoint - deltaPastPoint );

            if ( deltaScore < bestScore ) {
                bestScore = deltaScore;
                currentPos = SafeProgress ( i );
            }

            deltaPastPoint = deltaPoint;
        }   

        targetLink = PathGeneratorFunction ( currentPos );

        //Debug.DrawLine ( targetLink, targetLink + ( PathGeneratorFunction ( SafeProgress ( currentPos + stepsize ) ) - targetLink ).normalized * 50, Color.blue );
        //Debug.DrawLine ( transform.position, targetLink );

        base.Update ();
    }
}
