using UnityEngine;

public class LRCTM : TM {
    public  Vector2[]     positionPoints;
    public  Vector2[]     controlPoints;

    public  float           stepsize;

    public  float           searchRange;

    public  float           STRTargetForwardSeek;       // Pathfind velocity offset
    public  float           STRTargetForwardCenterCounterseek;      // Targeting velocity offset
    public  float           STRApproachVCounter;      // Targeting velocity offset

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
        for ( float i = 0; i < controlPoints.Length; i += stepsize * 100 ) {
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
        DrawCourse ();

        float bestScore = Mathf.Infinity;
        float deltaScore;

        for ( float i = currentPos - ( bidirectional ? searchRange : 0 ); i < currentPos + searchRange; i += stepsize ) {
            deltaScore = ( transform.position - (Vector3)PathGeneratorFunction ( SafeProgress ( i ) ) ).sqrMagnitude;
            if ( deltaScore < bestScore ) {
                bestScore = deltaScore;
                currentPos = SafeProgress ( i );
            }
        }

        targetLink = PathGeneratorFunction ( currentPos );

        Debug.DrawLine ( transform.position, PathGeneratorFunction ( currentPos ) );

        base.Update ();
    }
}
