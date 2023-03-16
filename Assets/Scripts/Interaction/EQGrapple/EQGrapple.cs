using UnityEngine;

public class EQGrapple : TriggerAssembly {
    public  GrappleHead head;

    public  AimHelper   aimHelper;
    public  Transform   aimHelperTarget;
    public  Transform   aimHelperBase;

    public  float       launchSpeed;
    public  float       maxRange;
    public  bool        headLaunched;

    public  float       velocityDampeningStrength;
    public  float       linearSpringStrength;

    public  float       orbitalDampeningStrength;
    public  float       orbitalDampeningPower;

    private Vector3     pastTetherHeading;
    private Vector2     forceAccumulator;

    private  LineRenderer    lineRenderer;

    public override void Start() {
        SetRGB( transform.parent.parent.GetComponent<Rigidbody2D>() );
        lineRenderer = GetComponent<LineRenderer>();
        head.SetAnchorParam( transform, maxRange );
        base.Start();
    }

    public override void Update () {
        if ( Input.GetAxis ("Fire2") > 0 ) {
            TriggerHold ();
        } else {
            aimHelper.LockIn ( aimHelperBase );
            TriggerRelease ();
        }

        if ( deltaA == 0 ) Reload ();

        base.Update ();

    }

    private void LateUpdate() {
        if ( headLaunched && head.gameObject.activeInHierarchy ) {
            lineRenderer.enabled = true;
            Vector2 delta   = head.transform.position - transform.position;
            float   deltaR  = ( delta.magnitude / head.attachLength );
            deltaR *= deltaR;
            Color deltaC    = Color.Lerp ( Color.black, Color.red, deltaR );
            lineRenderer.startColor = deltaC;
            lineRenderer.endColor   = deltaC;
            lineRenderer.SetPosition( 0, transform.position );
            lineRenderer.SetPosition( 1, head.transform.position );
        } else {
            lineRenderer.enabled = false;
        }
    }

    private void FixedUpdate() {
        if ( headLaunched ) {
            if ( triggerDown ) {
                if ( !head.detached && head.gameObject.activeInHierarchy ) {
                    aimHelper.LockIn ( aimHelperTarget );
                    Vector2 delta   = head.transform.position - transform.position;
                    if ( delta.magnitude > head.attachLength ) {
                        Vector2 deltaN  = delta.normalized;
                        Vector2 deltaV  = rgb.velocity - head.Interogate();
                        Vector2 deltaT  = Vector3.Project( deltaV, Quaternion.Euler( 0, 0, 90 ) * deltaN );
                        float   deltaS  = Vector3.Dot ( deltaV, deltaN );

                        forceAccumulator =  deltaN * ( ( delta.magnitude - head.attachLength ) * linearSpringStrength - velocityDampeningStrength * deltaS ) * Time.fixedDeltaTime;
                        forceAccumulator -= deltaT.normalized * Mathf.Pow( deltaT.magnitude, orbitalDampeningPower ) * orbitalDampeningStrength;

                        if ( Vector2.Dot ( forceAccumulator, transform.position - head.transform.position ) > 0 ) {
                            forceAccumulator -= (Vector2)Vector3.Project ( forceAccumulator, transform.position - head.transform.position );
                            forceAccumulator = Vector3.zero;
                        }

                        rgb.AddForce ( forceAccumulator, ForceMode2D.Impulse );
                        head.Propagate ( -forceAccumulator * rgb.mass );
                    }
                    pastTetherHeading = delta;
                }
            } else {
                head.gameObject.SetActive( false );
                headLaunched = false;
            }
             
            //rgb.transform.Rotate ( Vector3.forward, angleAccumulator );

            forceAccumulator = Vector3.zero;
        }
    }

    public override void Fire( Vector2 prv ) {
        headLaunched = true;
        head.transform.SetParent( head.savedParent );
        head.transform.position = transform.position + transform.up * ( 2 + rgb.velocity.magnitude * Time.fixedDeltaTime * 2 );
        head.transform.rotation = transform.rotation;
        head.gameObject.SetActive( true );
        head.GetComponent<Rigidbody2D>().velocity = ( Vector3 ) prv + transform.up * launchSpeed;
    }
}
