using UnityEngine;

public class PayloadExplosion : PayloadCore {
    public  int     damage;
    public  float   range;
    public  float   ekSTR;      // Explosion knockback STR

    public override void Deploy ( PayloadObject _instructions ) {
        Collider2D[] delta = Physics2D.OverlapAreaAll ( 
            transform.position - new Vector3 ( range, range ), 
            transform.position + new Vector3 ( range, range ),
            ~LayerMask.GetMask ( LayerMask.LayerToName ( gameObject.layer ) ) );

        Hitbox dhb;
        for ( int i = 0; i < delta.Length; i++ ) {
            if ( delta [ i ].TryGetComponent( out dhb ) ) {
                dhb.DeltaF ( damage );
            }
            if ( delta [ i ].attachedRigidbody != null ) {
                delta [ i ].attachedRigidbody.AddForce ( ( delta [ i ].transform.position - transform.position ).normalized * ekSTR );
            }
        }

        base.Deploy ( _instructions );
        Store ();
    }
}
