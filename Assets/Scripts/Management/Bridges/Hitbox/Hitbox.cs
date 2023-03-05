using UnityEngine;

public class Hitbox : MonoBehaviour {
    private void EvWrapper ( GameObject col ) {
        Hitgen delta = col.GetComponent<Hitgen>();
        if ( delta != null ) {
            DeltaF ( delta.Bump() );
        }
    }
    
    public virtual void DeltaF ( int a ) {}
         
    private void OnTriggerEnter2D( Collider2D collision ) {
        EvWrapper( collision.gameObject );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        EvWrapper( collision.gameObject );
    }
}
