using UnityEngine;

public class PowerNode : MonoBehaviour {
    public  PowerCord[]     conn;
    public  PowerCell[]     batt;

    public  float   load;
    public  float   magic1;
    public  float   magic2;
    private float   compound;

    public  float   reqLoad;

    private void Start () {
    }

    void Update () {
        load    = Mathf.Max ( 0, reqLoad );
        magic1  = -Mathf.Min ( 0, reqLoad );
        magic2  = 0;
        for ( int i = 0; i < conn.Length; i++ ) {
            load += Mathf.Max ( 0, conn [ i ].Interogate ( this ) );
            magic1 -= Mathf.Min ( 0, conn [ i ].Interogate ( this ) );
        }

        for ( int i = 0; i < batt.Length && load > magic1; i++ ) {
            magic2 += batt [ i ].VariDrain ( ( load - magic1 - magic2 ) * Time.deltaTime ) / Time.deltaTime;
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
}
