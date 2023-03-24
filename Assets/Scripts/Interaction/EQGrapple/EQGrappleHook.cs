using UnityEngine;

public class EQGrappleHook : MonoBehaviour {
    private EQGrapple       grappleCore;
    private Rigidbody2D     rgb;

    public Transform       anchor;
    private float           anchorMaxRange;

    private Transform       attachedTransform;
    private Rigidbody2D     attachedRGB;

    public Transform       savedParent;
    public  bool            detached;
    public  float           attachLength;

    public  Radar       attachRadar;
    public  Radar       gravRadar;

    public  float       STRGrav;

    private void OnGrappleAttach () {
        if ( attachedTransform == null ) return;
        detached = false;
        attachedTransform.TryGetComponent ( out attachedRGB );
        if ( attachedRGB == null ) {
            if ( !attachedTransform.TryGetComponent ( out attachedRGB ) ) {
                attachedRGB = attachedTransform.GetComponentInParent<Rigidbody2D> ();
            }
        }
        ZethaMinion deltaM = attachedTransform.GetComponent<ZethaMinion> ();
        if ( deltaM ) {
            deltaM.controller.TryGetComponent ( out attachedRGB );
        }
        if ( attachedRGB != null ) {
            HSTM deltaHT;
            if ( attachedRGB.TryGetComponent ( out deltaHT ) ) {
                deltaHT.Bind ( null );
            }
            EQGrappleAP deltaAP; 
            if ( attachedTransform.TryGetComponent ( out deltaAP ) ) {
                transform.localPosition = deltaAP.GetPoint ();
            }
            attachedRGB.gameObject.layer = anchor.gameObject.layer;
        }
        grappleCore.HookAttach ( attachedRGB );
    }

    public void BaseInit ( Transform _anchor, float _anchorMaxRange, EQGrapple _grappleCore ) {
        anchor = _anchor; anchorMaxRange = _anchorMaxRange; grappleCore = _grappleCore;
    }

    private void Start () {
        savedParent = transform.parent;
        rgb = GetComponent<Rigidbody2D> ();
        gameObject.SetActive ( false );
    }

    void OnEnable () {
        detached = true;
        if ( savedParent == null ) { savedParent = transform.parent; }
        GetComponent<Rigidbody2D> ().isKinematic = false;
        attachedRGB = null;
    }

    private void OnDisable () {
        rgb.isKinematic = true;
        rgb.velocity = Vector2.zero;
        attachLength = 0;
        attachRadar.Clear ();
        gravRadar.Clear ();
        attachedTransform = null;
        grappleCore.HookDetach ();
    }

    void Update () {
        if ( detached ) {
            if ( ( transform.position - anchor.position ).magnitude > anchorMaxRange ) {
                grappleCore.HookDetach ();
            }
            if ( attachRadar.collectedCount != 0 ) {
                attachLength = ( transform.position - anchor.position ).magnitude;

                attachedTransform = attachRadar.collectedColliders [ 0 ].transform;
                transform.SetParent ( attachedTransform );
                transform.position = Physics2D.ClosestPoint ( transform.position, attachRadar.collectedColliders [ 0 ] );

                rgb.isKinematic = true;
                rgb.velocity = Vector2.zero;

                OnGrappleAttach ();
            }
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

    public void ResetParent () {
        if ( transform != null && savedParent != null ) {
            transform.parent = savedParent;
        }
    }
}
