using UnityEngine;

public class Hitbox : MonoBehaviour {
    public  PoolSpooler     ps;

    public  void    TriggerHitEffect ( Transform impactor ) {
        GameObject      deltaGO = ps.Request();
        ParticleSystem  deltaPS = deltaGO.GetComponent<ParticleSystem>();
        deltaGO.SetActive ( true );

        if ( deltaPS != null ) {
            Vector2 deltaP;
            Vector2 deltaN;

            deltaP = Physics2D.ClosestPoint ( impactor.position, GetComponent<Collider2D> () );
            deltaN = impactor.position - transform.position;

            var deltaShape = deltaPS.shape;
            deltaShape.position = deltaP;
            deltaShape.rotation = transform.worldToLocalMatrix * new Vector3 ( 0, 0, ( 180 - deltaPS.shape.arc ) / 2 + Vector2.Angle ( Vector2.up, deltaN ) );
            deltaPS.Play ();
        }
    }

    private void EvWrapper ( GameObject deltaOBJ, bool wasTrigger = false, Vector2 ? deltaV = null ) {
        Hitgen delta = deltaOBJ.GetComponent<Hitgen>();
        if ( delta != null ) {
            if ( wasTrigger ) TriggerHitEffect ( deltaOBJ.transform );
            DeltaF ( delta.Bump ( gameObject, deltaV ) );
        }
    }

    public virtual void DeltaF ( int a ) {}
         
    private void OnTriggerEnter2D( Collider2D collider ) {
        EvWrapper ( collider.gameObject, collider.isTrigger );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        TriggerHitEffect ( collision.transform );
        EvWrapper ( collision.collider.gameObject );
    }
}
