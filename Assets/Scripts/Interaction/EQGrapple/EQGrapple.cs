using UnityEngine;

public class EQGrapple : TriggerAssembly {
    public  LineConnector   lineConn;

    public  AimHelper   aimHelper;
    public  Transform   aimHelperTarget;
    public  Transform   aimHelperBase;

    public  EQGrappleHook hookLink;

    public  float       launchSpeed;
    public  float       maxRange;
    public  bool        headLaunched;

    public  float       velocityDampeningStrength;
    public  float       linearSpringStrength;

    public  float       orbitalDampeningStrength;
    public  float       orbitalDampeningPower;

    private Vector2     forceAccumulator;

    public override void Start () {
        SetRGB ( transform.parent.parent.GetComponent<Rigidbody2D> () );
        hookLink.SetAnchorParam ( transform, maxRange );
        lineConn.objectA = transform;
        lineConn.objectB = hookLink.transform;
        lineConn.enabled = false;
        hookLink.Bind ( this );
        base.Start ();
    }

    public override void Update () {
        if ( Input.GetAxis ( "Fire2" ) > 0 ) {
            TriggerHold ();
        } else {
            aimHelper.LockIn ( aimHelperBase );
            TriggerRelease ();
        }
        lineConn.enabled = triggerDown && headLaunched && hookLink.gameObject.activeInHierarchy;

        if ( deltaA == 0 ) Reload ();

        base.Update ();
    }

    private void FixedUpdate () {
        if ( headLaunched ) {
            if ( triggerDown ) {
                if ( !hookLink.detached && hookLink.gameObject.activeInHierarchy ) {
                    aimHelper.LockIn ( aimHelperTarget );

                    Vector2 delta   = hookLink.transform.position - transform.position;
                    if ( delta.magnitude > hookLink.attachLength ) {
                        Vector2 deltaDir    = delta.normalized;
                        Vector2 deltaV      = rgb.velocity - hookLink.Interogate();
                        Vector2 deltaVT     = Vector3.Project( deltaV, Quaternion.Euler( 0, 0, 90 ) * deltaDir );
                        float   deltaS      = Vector3.Dot ( deltaV, deltaDir );

                        forceAccumulator = deltaDir * ( ( delta.magnitude - hookLink.attachLength ) * linearSpringStrength - velocityDampeningStrength * deltaS ) * Time.fixedDeltaTime;
                        forceAccumulator -= deltaVT.normalized * Mathf.Pow ( deltaVT.magnitude, orbitalDampeningPower ) * orbitalDampeningStrength;

                        if ( Vector2.Dot ( forceAccumulator, transform.position - hookLink.transform.position ) > 0 ) {
                            forceAccumulator -= (Vector2) Vector3.Project ( forceAccumulator, transform.position - hookLink.transform.position );
                            forceAccumulator = Vector3.zero;
                        }

                        rgb.AddForce ( forceAccumulator, ForceMode2D.Impulse );
                        hookLink.Propagate ( -forceAccumulator * rgb.mass );
                    }
                }
            }
            forceAccumulator = Vector3.zero;
        }
    }

    public override GameObject Fire ( Vector2 prv ) {
        headLaunched = true;
        hookLink.transform.position = transform.position + transform.up * 2;
        hookLink.transform.rotation = transform.rotation;
        hookLink.gameObject.SetActive ( true );
        hookLink.GetComponent<Rigidbody2D> ().velocity = (Vector3) prv + transform.up * launchSpeed;
        return null;
    }

    public override void TriggerRelease () {
        hookLink.ResetParent ();
        hookLink.gameObject.SetActive ( false );
        headLaunched = false;
        lineConn.attachLength = 0;
        lineConn.enabled = false;

        base.TriggerRelease ();
    }

    private void OnDisable () {
        TriggerRelease ();
    }

    public  void    HookAttach () {
        lineConn.attachLength = hookLink.attachLength;
        lineConn.enabled = true;
    }
}
