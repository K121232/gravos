using UnityEngine;

public class Hitgen : MonoBehaviour {
    public  int     damageBase;
    private Vector3 deltaPos = Vector3.zero;

    public void OnEnable () {
        deltaPos = transform.position;
    }

    private void Update () {
        RaycastHit2D[] hits;
        Hitbox delta;
        if ( transform.position.y > 0) {
        }
        hits = Physics2D.RaycastAll ( transform.position, deltaPos - transform.position, ( deltaPos - transform.position ).magnitude );
        Debug.DrawLine ( transform.position, deltaPos );
        for ( int i = 0; i < hits.Length; i++ ) {
            if ( hits [ i ].transform.TryGetComponent( out delta )  ) {
                Debug.Log ( "Line caught something" );
                delta.Superwrapper ( GetComponent<Collider2D>() );
            }
        }
        deltaPos = transform.position;
    }

    public  virtual int    Bump ( GameObject who = null, Vector2? deltaV = null ) {
        return damageBase;
    }
}
