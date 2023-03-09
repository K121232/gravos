using UnityEngine;

public class DebugSOD : MonoBehaviour {
    public  float   f, z, r;
    private float   yp = 0;

    float YF ( float x ) {
        if ( x > 5 ) return 0;
        if ( x > 1 ) return 5;
        return 0;
    }

    void Update () {
        SOD sd = new( f, z, r, 0 );

        Vector2 delta = Vector2.zero, delta2;
        yp = 0;
        for ( float i = 0; i < 10; i += 0.01f ) {
            yp = sd.Update ( 0.01f, YF ( i ), delta.y );
            delta2 = new Vector2 ( i, yp );

            Debug.DrawLine ( delta, delta2, Color.red );
            Debug.DrawLine ( new Vector2 ( delta.x, YF ( delta.x ) ), new Vector2 ( delta2.x, YF ( delta2.x ) ), Color.white );

            delta = delta2;
        }
    }
}
