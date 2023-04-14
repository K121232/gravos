using UnityEngine;

public class PowerNode : MonoBehaviour {
    public  PowerCord[]     conn;
    public  PowerCell[]     batt;

    public  float   load;
    public  float   magic1;
    public  float   magic2;
    private float   compound;

    public  float   reqLoad;

    public  float   cellMaximumLoad;
    public  float   cellAvailableLoad;

    private float   lastUpdateTime;

    private void Start () {
        for ( int i = 0; i < batt.Length; i++ ) {
            cellMaximumLoad += batt [ i ].resourceMax;
        }
    }

    public  void    ForceSync () {
        FixedUpdate ();
    }

    void FixedUpdate () {
        if ( lastUpdateTime == Time.time ) return;
        lastUpdateTime = Time.time;

        cellAvailableLoad = 0;
        load = Mathf.Max ( 0, reqLoad );
        magic1 = -Mathf.Min ( 0, reqLoad );
        magic2 = 0;
        for ( int i = 0; i < conn.Length; i++ ) {
            load += Mathf.Max ( 0, conn [ i ].Interogate ( this ) );
            magic1 -= Mathf.Min ( 0, conn [ i ].Interogate ( this ) );
        }

        for ( int i = 0; i < batt.Length; i++ ) {
            if ( load > magic1 + magic2 ) {
                magic2 += batt [ i ].VariDrain ( ( load - magic1 - magic2 ) * Time.fixedDeltaTime ) / Time.fixedDeltaTime;
            }
            cellAvailableLoad += batt [ i ].resourceCurrent;
        }

        float delta;

        compound = magic1 + magic2;
        for ( int i = 0; i < conn.Length; i++ ) {
            delta = conn [ i ].Interogate ( this );
            if ( delta > 0 ) {
                delta = Mathf.Min ( delta, compound );
                conn [ i ].Propagate ( this, -delta );
                compound -= delta;
            }
        }

        compound = Mathf.Max ( 0, load - magic2 );
        for ( int i = 0; i < conn.Length; i++ ) {
            delta = conn [ i ].Interogate ( this );
            if ( delta <= 0 ) {
                delta = compound;
                conn [ i ].Propagate ( this, delta );
                compound -= delta;
            }
        }

    }

    public void TrimDrain ( float delta ) {
        reqLoad += delta;
    }

    public  float   GetLoad () {
        return load - magic1 - magic2;
    }
}
