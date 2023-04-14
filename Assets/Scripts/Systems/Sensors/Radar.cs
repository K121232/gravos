using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

    public  List<Collider2D>    collectedColliders;
    public  int                 collectedCount;

    public virtual void Start() {
        collectedColliders = new List<Collider2D>( 16 );
        collectedCount = 0;
    }

    public virtual void Update() {
        collectedCount = collectedColliders.Count;
    }

    virtual public void Clear() {
        collectedColliders.Clear();
        collectedCount = 0;
    }
}
