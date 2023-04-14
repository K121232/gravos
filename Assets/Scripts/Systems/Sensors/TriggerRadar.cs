using UnityEngine;
using System.Collections.Generic;

public class TriggerRadar : Radar {
    public  string[]    maskTag;
    public  bool        invertMaskTag;
    private List<Collider2D>    persistent;
    private Collider2D          cc;
    public override void Start() {
        persistent = new List<Collider2D>( 16 );
        cc = GetComponent<Collider2D>();
        base.Start();
    }

    public override void Update() {
        Clear();
        for ( int i = 0; i < persistent.Count; i++ ) {
            if ( !persistent [ i ].gameObject.activeInHierarchy ) {
                persistent.RemoveAt( i );
                i--;
                continue;
            }
            collectedColliders.Add( persistent [ i ] );
        }
        base.Update();
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if ( !collision.isTrigger && collision.IsTouching ( cc ) ) {
            for ( int i = 0; i < maskTag.Length; i++ ) {
                if ( collision.gameObject.CompareTag ( maskTag [ i ] ) ) {
                    if ( invertMaskTag ) { persistent.Add ( collision ); break; }
                    else return;
                }
            }
            if ( !invertMaskTag ) persistent.Add ( collision );
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        persistent.Remove( collision );
    }
}
