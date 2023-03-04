using System.Collections.Generic;
using UnityEngine;

public class ZoneRadar : Radar {
    public  float       angleOffset;
    public  float       angle;

    public  LayerMask   mask;
    public  string[]    maskTag;

    private List<Collider2D>    deltaColl;

    public  Collider2D          coll;
    private ContactFilter2D     conFilter;

    public override void Start() {
        deltaColl = new List<Collider2D>( 16 );
        conFilter = new ContactFilter2D();
        conFilter.SetLayerMask( mask );
        base.Start();
    }

    public override void Update() {
        Clear();

        coll.OverlapCollider( conFilter, deltaColl );
        if ( angle < 0 ) {
            collectedColliders = deltaColl;
        } else {
            for ( int i = 0; i < deltaColl.Count; i++ ) {
                if ( Vector2.Angle( Quaternion.Euler( 0, 0, angleOffset ) * transform.up, deltaColl[i].transform.position - transform.position ) < angle ) {
                    collectedColliders.Add( deltaColl[i] );
                }
            }
        }
        for ( int i = 0; i < maskTag.Length; i++ ) {
            for ( int j = 0; j < collectedColliders.Count; j++ ) {
                if ( collectedColliders [ j ].CompareTag ( maskTag [ i ] ) ) {
                    collectedColliders.RemoveAt ( j );
                    j--;
                }
            }
        }
        collectedCount = collectedColliders.Count;
    }

    public override void Clear () {
        deltaColl.Clear();
        base.Clear();
    }
}
