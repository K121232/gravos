using UnityEngine;
using System.Collections.Generic;

public class PoolSpooler : MonoBehaviour {
    public GameObject   generator;
    public int          expected;
    public  int         pos;

    private List<GameObject>  pool;

    public virtual void ItemOnStart ( GameObject alpha ) {
        alpha.transform.SetParent ( transform );
        alpha.SetActive ( false );
    }

    public virtual void Start() {
        pool = new List<GameObject>( expected );
        for ( int i = 0; i < expected; i++ ) {
            GameObject delta = Instantiate( generator );
            ItemOnStart ( delta );
            pool.Add ( delta );
        }
        pos = 0;
    }

    public virtual GameObject Request () {
        int delta = pos;
        pos = ( pos + 1 ) % expected;
        pool [ delta ].SetActive ( false );
        return pool [ delta ];
    }
}
