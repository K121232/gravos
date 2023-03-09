using UnityEngine;

public class DebugSOD : MonoBehaviour {
    public  float   f, z, r;
    private float   yv = 0, yp = 0;
    void Update () {
        SOD sd = new SOD ( f, z, r, 0 );
        Vector2 delta = Vector2.zero, delta2;
        yv = 0; yp = 0;
        for ( float i = 0; i < 10; i += 0.01f ) {

            yv += sd.Update ( 0.02f, ( i < 1 ? 0 : 1 ) - delta.y, i < 1 ? 0 : 1, yv ) * 0.02f;
            yp += yv * 0.02f;

            delta2 = new Vector2 ( i, yp );
            Debug.DrawLine ( delta, delta2, Color.red );
            Debug.DrawLine ( new Vector2 ( delta.x, delta.x < 1 ? 0 : 1 ), new Vector2 ( delta2.x, delta2.x < 1 ? 0 : 1 ), Color.white );
            delta = delta2;
        }
    }
}
