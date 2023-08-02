using UnityEngine;

public class Hitgen : MonoBehaviour {
    public  int     damageBase;
    public  bool    autoline = true;
    protected Vector3 deltaPos = Vector3.zero;

    public  PayloadCore detonatePayloadFasttrack;

    RaycastHit2D[] hits;
    Hitbox delta;

    public void OnEnable () {
        if ( detonatePayloadFasttrack == null ) {
            detonatePayloadFasttrack = GetComponent<PayloadStart> ();
        }
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
            if ( detonatePayloadFasttrack is PayloadStart ) {
                ( detonatePayloadFasttrack as PayloadStart ).EarlyDetonate ();
            } else {
                detonatePayloadFasttrack.Deploy ( new PayloadObject () );
            }
        }
        return damageBase;
    }
}
