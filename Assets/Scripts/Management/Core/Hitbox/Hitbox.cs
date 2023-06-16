using UnityEngine;

public class Hitbox : MonoBehaviour {
    public  PayloadFXD  dispenser;

    public  void    TriggerHitEffect ( Transform impactor ) {
        dispenser.Deploy ( new PayloadObject ( Vector2.zero, ( impactor.position - transform.position ).normalized, null ) );
    }

    private void EvWrapper ( GameObject deltaOBJ, bool wasTrigger = false, Vector2 ? deltaV = null ) {
        Hitgen delta = deltaOBJ.GetComponent<Hitgen>();
        if ( delta != null ) {
            if ( wasTrigger ) TriggerHitEffect ( deltaOBJ.transform );
            DeltaF ( delta.Bump ( gameObject, deltaV ) );
        }
    }

    public virtual void DeltaF ( int a ) {}
         
    public void Superwrapper ( Collider2D alpha, bool hitEffect = false ) {
        if ( hitEffect ) { TriggerHitEffect ( alpha.transform ); }
        EvWrapper ( alpha.gameObject, alpha.isTrigger );
    }

    private void OnTriggerEnter2D( Collider2D collider ) {
        Superwrapper ( collider );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        Superwrapper ( collision.collider, true );
    }
}
