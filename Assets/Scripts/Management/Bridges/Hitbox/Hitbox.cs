using UnityEngine;

public class Hitbox : MonoBehaviour {
    private ParticleSystem  ps;

    private void EvWrapper ( GameObject deltaOBJ, Collision2D col ) {
        Hitgen delta = deltaOBJ.GetComponent<Hitgen>();
        if ( delta != null ) {
            DeltaF ( delta.Bump () );

            Vector2 deltaP;
            Vector2 deltaN;

            deltaP = Physics2D.ClosestPoint ( deltaOBJ.transform.position, GetComponent<Collider2D> () ) - (Vector2) transform.position;
            deltaN = deltaOBJ.transform.position - transform.position;

            if ( ps != null ) {
                var deltaShape = ps.shape;
                deltaShape.position = deltaP;
                deltaShape.rotation = transform.worldToLocalMatrix * new Vector3 ( 0, 0, ( 180 - ps.shape.arc ) / 2 + Vector2.Angle ( Vector2.up, deltaN ) );
                ps.Play ();
            }
        }
    }

    public virtual void Start () {
        ps = GetComponent<ParticleSystem> ();
    }

    public virtual void DeltaF ( int a ) {}
         
    private void OnTriggerEnter2D( Collider2D collision ) {
        EvWrapper ( collision.gameObject, null );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        EvWrapper( collision.gameObject, collision );
    }
}
