using System;
using UnityEngine;

public class SOD {
    public  float   xp;
    public  float   k1, k2, k3;
    private float   yd;

    public SOD ( float f, float z, float r, float x0 ) {
        float PIF = (float)Math.PI * f;
        k1 = z / PIF;
        k2 = 1 / ( 4 * PIF * PIF );
        k3 = r * z / ( 2 * PIF );
        xp = x0;
        yd = 0;
    }

    public float Update ( float T, float delta, float ax, float ayd ) {
        float xd = ( ax - xp ) / T;
        xp = ax;

        float k2_stable = Math.Max ( k2, 1.1f * ( T * T / 4 + T * k1 / 2 ) );

        float yacc = T * ( delta + k3 * xd - k1 * ayd ) / k2_stable;

        return yacc;
    }
    public float Update ( float T, float delta, float ax ) {
        float yacc = Update ( T, delta, ax, yd );
        return yacc;
    }
    public  void   UpdateYD ( float a ) {
        yd += a;
    }
}
