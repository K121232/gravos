using UnityEngine;

public class EQGrappleHook : MonoBehaviour {
    private Rigidbody2D     rgb;

    public Transform       anchor;
    public Rigidbody2D     attachedRGB;
    private float           anchorMaxRange;

    public Transform       savedParent;
    public  bool            detached;
    public  float           attachLength;
    public  float           minAttachLength;

    public  Radar       attachRadar;
    public  Radar       gravRadar;

    public  float       STRGrav;

    private void OnGrappleAttach () {
        Transform   deltaRF = attachRadar.collectedColliders [ 0 ].transform;
        deltaRF.TryGetComponent ( out attachedRGB );
        if ( attachedRGB == null ) {
            if ( !deltaRF.TryGetComponent ( out attachedRGB ) ) {
                attachedRGB = deltaRF.GetComponentInParent<Rigidbody2D> ();
            }
        }
        ZethaMinion deltaM = deltaRF.GetComponent<ZethaMinion> ();
        if ( deltaM ) {
            deltaM.controller.TryGetComponent ( out attachedRGB );
        }
        if ( attachedRGB != null ) {
            HSTM deltaHT;
            if ( attachedRGB.TryGetComponent( out deltaHT) ) {
                deltaHT.Bind ( null );
            }
            EQGrappleAP deltaAP;
            if ( deltaRF.TryGetComponent ( out deltaAP ) ) {
                transform.localPosition = deltaAP.GetPoint ();
            }
            attachedRGB.gameObject.layer = anchor.gameObject.layer;
        }
    }

    public void SetAnchorParam ( Transform a, float b ) {
        anchor = a; anchorMaxRange = b;
    }

    private void Start () {
        savedParent = transform.parent; 
        rgb = GetComponent<Rigidbody2D> ();
        gameObject.SetActive ( false );
    }

    void OnEnable () {
        if ( savedParent == null ) { savedParent = transform.parent; }
        GetComponent<Rigidbody2D> ().isKinematic = false;
        detached = true;
        attachedRGB = null;
        detached = true;
    }

    private void OnDisable () {
        detached = true;
        rgb.isKinematic = true;
        rgb.velocity = Vector2.zero;
        attachLength = 0;
        attachRadar.Clear ();
    }

    void Update () {
        if ( detached && ( transform.position - anchor.position ).magnitude > anchorMaxRange ) {
            transform.SetParent ( savedParent );
            gameObject.SetActive ( false );
        }
        if ( detached && attachRadar.collectedCount != 0 ) {
            detached = false;
            attachLength = Mathf.Max ( minAttachLength, ( transform.position - anchor.position ).magnitude );

            transform.SetParent ( attachRadar.collectedColliders [ 0 ].transform );
            transform.position = Physics2D.ClosestPoint ( transform.position, attachRadar.collectedColliders [ 0 ] );

            rgb.isKinematic = true;
            rgb.velocity = Vector2.zero;

            OnGrappleAttach ();
        }
    }

    private void FixedUpdate () {
        if ( detached ) {
            for ( int i = 0; i < gravRadar.collectedCount; i++ ) {
                Vector2 deltaG = ( gravRadar.collectedColliders[i].transform.position - transform.position );
                rgb.AddForce ( deltaG.normalized * deltaG.sqrMagnitude * STRGrav, ForceMode2D.Impulse );
            }
        }
    }

    public void Propagate ( Vector2 force ) {
        if ( attachedRGB != null ) {
            attachedRGB.AddForceAtPosition ( force, transform.position );
        }
    }

    public  void    ResetParent () {
        transform.parent = savedParent;
    }

    public Vector2 Interogate () {
        if ( attachedRGB != null ) {
            return attachedRGB.velocity;
        }
        return Vector2.zero;
    }

    public Rigidbody2D GetAnchorRGB () {
        return attachedRGB;
    }
}
