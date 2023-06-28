using UnityEngine;

public class Hitgen : MonoBehaviour {
    public  int     damageBase;
    public  bool    autoline = true;
    protected Vector3 deltaPos = Vector3.zero;

    public  PayloadStart detonatePayloadFasttrack;

    RaycastHit2D[] hits;
    Hitbox delta;

    public void OnEnable () {
        detonatePayloadFasttrack = GetComponent<PayloadStart> ();
        deltaPos = transform.position;
    }

    public virtual void Update () {
        if (!autoline) { return; }
        hits = Physics2D.RaycastAll ( 
            transform.position, 
            deltaPos - transform.position, ( deltaPos - transform.position ).magnitude, 
            ~LayerMask.GetMask ( LayerMask.LayerToName ( gameObject.layer ) ) 
        );
        for ( int i = 0; i < hits.Length; i++ ) {
            if ( hits [ i ].transform.TryGetComponent( out delta )  ) {
                delta.Superwrapper ( GetComponent<Collider2D>() );
                break;
            }
        }
        deltaPos = transform.position;
    }

    public  virtual int    Bump ( GameObject who = null, Vector2? deltaV = null ) {
        if ( detonatePayloadFasttrack != null ) {
            detonatePayloadFasttrack.EarlyDetonate ();
        }
        return damageBase;
    }
}
