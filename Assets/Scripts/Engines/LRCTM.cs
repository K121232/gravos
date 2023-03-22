using UnityEngine;

public class LRCTM : TM {
    public  Vector2[]     points;
    public  float           resolution;

    public  float           scanDistance;

    private int             major = 0;
    private float           minor = 0;

    public  float           STRTargetForwardSeek;       // Pathfind velocity offset
    public  float           STRTargetForwardCenterCounterseek;      // Targeting velocity offset
    public  float           STRApproachVCounter;      // Targeting velocity offset

    public  Transform       center;
    private Rigidbody2D     centerRGB;
    private Vector2         offset = new( 0, 0 );

    public Vector2 Gitmas ( int a2, float a1 ) {
        float ma = 1 - a1;
        return offset + points [ a2 ] * ma * ma * ma
                + 3 * (
                    ma * ma * a1 * ( points [ a2 ] + points [ a2 + 1 ] * ( ( a2 / 2 ) % 2 == 0 ? 1 : -1 ) ) +
                    ma * a1 * a1 * ( points [ a2 + 2 ] + points [ a2 + 3 ] * ( ( a2 / 2 ) % 2 == 0 ? 1 : -1 ) )
                )
                + a1 * a1 * a1 * points [ a2 + 2 ];
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
        if ( a == null ) { center = null; centerRGB = null; return; }
        center = a;
        centerRGB = center.GetComponent<Rigidbody2D> ();
    }

    public override void Start () {
        LoadCenter ( center );        
        base.Start ();
    }

    public override void Update () {
        if ( center != null ) offset = center.position;

        DrawCourse ();

        float   bestScore = Mathf.Infinity;
        Vector2 deltaA, deltaB = new Vector2 ( major, minor );
        Vector2 target = transform.position + (Vector3)rgb.velocity * STRTargetForwardSeek;
        if ( centerRGB != null ) target -= centerRGB.velocity * STRTargetForwardCenterCounterseek;

        Vector2 DEBUGP = Vector2.zero;

        if ( scanDistance == 0 ) return;

        for ( float i = -scanDistance; i < scanDistance; i += resolution ) {
            int     md = ( major + Mathf.FloorToInt ( i + minor ) * 2 + points.Length - 2 ) % ( points.Length - 2 );
            float   mi = minor + i - Mathf.Floor ( minor + i );
            if ( mi < 0 ) mi += 1;
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

        targetLink = Gitmas ( major, minor ) - rgb.velocity * STRApproachVCounter;

                                                                Debug.DrawLine ( transform.position, target, Color.red );

        base.Update ();
    }
}
