using UnityEngine;

public class Hitgen : MonoBehaviour {
    public  int     damageBase;
    public  bool    autoline = true;
    private Vector3 deltaPos = Vector3.zero;

    public void OnEnable () {
        deltaPos = transform.position;
    }

    public virtual void Update () {
        if (!autoline) { return; }
        RaycastHit2D[] hits;
        Hitbox delta;
        if ( transform.position.y > 0) {
        }
        hits = Physics2D.RaycastAll ( transform.position, deltaPos - transform.position, ( deltaPos - transform.position ).magnitude, gameObject.layer );
        Debug.DrawLine ( transform.position, deltaPos );
        for ( int i = 0; i < hits.Length; i++ ) {
            if ( hits [ i ].transform.TryGetComponent( out delta )  ) {
                delta.Superwrapper ( GetComponent<Collider2D>() );
            }
        }
        deltaPos = transform.position;
    }

    public  virtual int    Bump ( GameObject who = null, Vector2? deltaV = null ) {
        return damageBase;
    }
}
