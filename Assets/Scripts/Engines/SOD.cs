using UnityEngine;

public class SOD {
    public  float   xv, xp;
    public  float   k1 = 0, k2 = 0, k3 = 0;
    private string  coreString;

    public SOD ( float f, float z, float r, float x0, bool angleMode = true ) {
        float PIF = Mathf.PI * f;
        k1 = z / PIF;
        k2 = 1f / ( 4f * PIF * PIF );
        k3 = r * z / ( 2 * PIF );

        xp = x0;
        xv = 0;
        coreString = $" {f}, {z}, {r} FZR -- K {k1} {k2} {k3}";
    }

    public  string Outcore  () {
        return coreString;
    }

    public  void PassThrough ( float _k1, float _k2, float _k3 ) {
        k1 = _k1;
        k2 = _k2;
        k3 = _k3;
    }

    public  float   Update ( float T, float delta, float x, float yv = Mathf.Infinity ) {
        if ( T == 0 ) return 0;
        xv = ( x - xp ) / T;
        xp = x;
        float k2s = Mathf.Max ( k2, 1.1f * ( T * T / 4 + T * k1 / 2 ) );
        
        if ( yv == Mathf.Infinity ) yv = 0;

        float ya = T * ( delta + k3 * xv - k1 * yv ) / k2s;

        return ya; 
    }
}
