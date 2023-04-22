using UnityEngine;

public class DataPlotter : MonoBehaviour {
    protected   float[] points;

    public      Color   lineColor = Color.white;

    public      float   lineHigh;
    public      float   lineLow;

    public      Vector2 spacing;

    protected   int     pos;
    protected   int     mx = 200;

    public      Vector2 offset;
    public      bool    anchored = true;

    public virtual void Start () {
        points = new float [ mx ];
        pos = 0;
    }

    protected   float   sample;

    void DrawScaled ( Vector2 a, Vector2 b, Color c ) {
        Vector2 delta = offset;
        if ( anchored ) {
            delta += (Vector2) transform.position;
        }
        Debug.DrawLine ( delta + Vector2.Scale ( spacing, a ), delta + Vector2.Scale ( spacing, b ), c );
    }

    public virtual void LateUpdate () {
        points [ pos ] = sample;
        pos = ( pos + 1 ) % mx;
        for ( int i = 0; i <= mx; i++ ) {
            DrawScaled ( new Vector2 ( i, points [ i % mx ] ), new Vector2 ( i + 1, points [ ( i + 1 ) % mx ] ), lineColor );
        }
        DrawScaled ( new Vector2 ( 0, 0 ), new Vector2 ( mx, 0 ), Color.white );
        DrawScaled ( new Vector2 ( 0, lineHigh ), new Vector2 ( mx, lineHigh ), Color.red );
        DrawScaled ( new Vector2 ( 0, lineLow ), new Vector2 ( mx, lineLow ), Color.red );
    }
}
