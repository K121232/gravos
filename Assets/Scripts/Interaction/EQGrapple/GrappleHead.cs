using UnityEngine;

public class GrappleHead : MonoBehaviour {
    private Rigidbody2D     rgb;

    private Transform       anchor;
    private Rigidbody2D     anchorRgb;
    private float           anchorMaxRange;

    public  Transform       savedParent;
    public  bool            detached;
    public  float           attachLength;
    public  float           minAttachLength;

    public  Radar       attachRadar;
    public  Radar       gravRadar;

    public  float       STRGrav;

    public void SetAnchorParam ( Transform a, float b ) {
        anchor = a; anchorMaxRange = b;
    }

    private void Start() {
        savedParent = transform.parent;
        rgb = GetComponent<Rigidbody2D>();
        gameObject.SetActive( false );
    }

    void OnEnable() {
        if ( savedParent == null ) { savedParent = transform.parent; }
        transform.SetParent( savedParent );
        GetComponent<Rigidbody2D>().isKinematic = false;
        detached = true;
        anchorRgb = null;
        detached = true;
    }

    private void OnDisable() {
        detached = true;
        rgb.isKinematic = true;
        rgb.velocity = Vector2.zero;
        attachLength = 0;
        attachRadar.Clear();
    }

    private Vector3     offset = Vector3.zero;
    private void DEBUGRange( float tg ) {
        Vector3 hand = Vector3.up * tg;
        int segments = 30;
        for ( int i = 0; i < segments; i++ ) {
            Debug.DrawLine( transform.position + hand - offset, transform.position + Quaternion.Euler( 0, 0, 360 / segments ) * hand - offset );
            hand = Quaternion.Euler( 0, 0, 360 / segments ) * hand;
        }
    }

    private void LateUpdate() {
        if ( !detached ) {
            DEBUGRange( attachLength );
        }
    }

    void Update() {
        if ( detached && ( transform.position - anchor.position ).magnitude > anchorMaxRange ) {
            transform.SetParent( savedParent );
            gameObject  .SetActive( false );
        }
        if ( detached && attachRadar.collectedCount != 0 ) {
            detached = false;

            attachLength = Mathf.Max ( minAttachLength, ( transform.position - anchor.position ).magnitude );
            transform.SetParent( attachRadar.collectedColliders[0].transform );
            transform.position = attachRadar.collectedColliders[0].ClosestPoint( transform.position );

            attachRadar.collectedColliders[0].transform.TryGetComponent( out anchorRgb );

            rgb.isKinematic = true;            
            rgb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        if ( detached ) {
            for ( int i = 0; i < gravRadar.collectedCount; i++ ) {
                Vector2 deltaG = ( gravRadar.collectedColliders[i].transform.position - transform.position );
                rgb.AddForce( deltaG.normalized * deltaG.sqrMagnitude * STRGrav, ForceMode2D.Impulse );
            }
        }
    }

    public void Propagate ( Vector2 force ) {
        if ( anchorRgb != null ) {
            anchorRgb.AddForceAtPosition( force / rgb.mass, transform.position );
        }
    }

    public Vector2 Interogate () {
        if ( anchorRgb != null ) {
            return anchorRgb.velocity;
        }
        return Vector2.zero;
    }

    public float InterogateMass () {
        if ( anchorRgb != null ) {
            return anchorRgb.mass;
        }
        return -1;
    }

    public  Rigidbody2D GetAnchorRGB() {
        return anchorRgb;
    }
}
