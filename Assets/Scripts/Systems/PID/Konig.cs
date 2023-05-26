using UnityEngine;

public class Konig : MonoBehaviour {
    public      float   f = 5, z = 1, r = 0;
    private     float   xv, xp;
    protected   float   k1 = 0, k2 = 0, k3 = 0;

    private void OnEnable () {
        float PIF = Mathf.PI * f;
        k1 = z / PIF;
        k2 = 1f / ( 4f * PIF * PIF );
        k3 = r * z / ( 2 * PIF );

        xp = 0;
        xv = 0;
        //coreString = $" {f}, {z}, {r} FZR -- K {k1} {k2} {k3}";
    }

    public  float   NextFrame ( float T, float delta, float x, float yv = Mathf.Infinity ) {
        if ( T == 0 ) return 0;
        xv = ( x - xp ) / T;
        xp = x;
        float k2s = Mathf.Max ( k2, 1.1f * ( T * T / 4 + T * k1 / 2 ) );
        
        if ( yv == Mathf.Infinity ) yv = 0;

        float ya = T * ( delta + k3 * xv - k1 * yv ) / k2s;

        return ya; 
    }
}
