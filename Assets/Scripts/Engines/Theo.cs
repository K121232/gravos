using UnityEngine;

public class Theo : MonoBehaviour {
    public  Vector2 xv, xp;
    public  float   f, z, r;
    private float   k1 = 0, k2 = 0, k3 = 0;
    private string  coreString;

    private void Start () {
        Init ( f, z, r );
        InitV ( Vector2.zero );
    }

    private void Update () {
        Init ( f, z, r );
    }

    public void Init ( float _f, float _z, float _r ) {
        float PIF = Mathf.PI * _f;
        k1 = _z / PIF;
        k2 = 1f / ( 4f * PIF * PIF );
        k3 = _r * _z / ( 2 * PIF );
        
        coreString = $" {_f}, {_z}, {_r} FZR -- K {k1} {k2} {k3}";
    }

    public void InitV ( Vector2 x0 ) {
        xp = x0;
        xv = Vector2.zero;
    }

    public Vector2 NextFrame ( float T, Vector2 delta, Vector2 x, Vector2 yv, Vector2 xvb ) {
        if ( T == 0 ) return Vector2.zero;
        xv = xvb + ( x - xp ) / T;
        xp = x;
        float k2s = Mathf.Max ( k2, 1.1f * ( T * T / 4 + T * k1 / 2 ) );

        Vector2 ya = T * ( delta + k3 * xv - k1 * yv ) / k2s;

        return ya;
    }
}
