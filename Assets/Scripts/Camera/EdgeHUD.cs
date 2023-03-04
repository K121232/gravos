using UnityEngine;
using System.Collections.Generic;

public class EdgeHUD : MonoBehaviour {
    public  List<Rigidbody2D>   tracked;
    private Rigidbody2D         rgb;

    private void Start() {
        rgb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate() {
        for ( int i = 0; i < tracked.Count; i++ ) {
            Vector3 lineout = ( tracked[i].transform.position - transform.position ).normalized;
            Debug.DrawLine( transform.position + lineout, transform.position + lineout * ( 1 + ( tracked[i].velocity - rgb.velocity ).magnitude / 10 ), Color.blue );
        }
    }
}
