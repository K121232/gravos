using UnityEngine;

public class EQGrapple : TriggerAssembly {
    public  LineConnector   lineConn;

    public  AimHelper   aimHelper;
    public  Transform   aimHelperTarget;
    public  Transform   aimHelperBase;

    public  EQGrappleHook head;

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
        head.SetAnchorParam ( transform, maxRange );
        lineConn.objectA = transform;
        lineConn.objectB = head.transform;
        lineConn.enabled = false;
        base.Start ();
    }

    public override void Update () {
        if ( Input.GetAxis ( "Fire2" ) > 0 ) {
            TriggerHold ();
        } else {
            aimHelper.LockIn ( aimHelperBase );
            TriggerRelease ();
        }
        lineConn.enabled = triggerDown && headLaunched && head.gameObject.activeInHierarchy;

        if ( deltaA == 0 ) Reload ();

        base.Update ();
    }

    private void FixedUpdate () {
        if ( headLaunched ) {
            if ( triggerDown ) {
                if ( !head.detached && head.gameObject.activeInHierarchy ) {
                    lineConn.attachLength = head.attachLength;
                    aimHelper.LockIn ( aimHelperTarget );

                    Vector2 delta   = head.transform.position - transform.position;
                    if ( delta.magnitude > head.attachLength ) {
                        Vector2 deltaDir    = delta.normalized;
                        Vector2 deltaV      = rgb.velocity - head.Interogate();
                        Vector2 deltaVT     = Vector3.Project( deltaV, Quaternion.Euler( 0, 0, 90 ) * deltaDir );
                        float   deltaS      = Vector3.Dot ( deltaV, deltaDir );

                        forceAccumulator = deltaDir * ( ( delta.magnitude - head.attachLength ) * linearSpringStrength - velocityDampeningStrength * deltaS ) * Time.fixedDeltaTime;
                        forceAccumulator -= deltaVT.normalized * Mathf.Pow ( deltaVT.magnitude, orbitalDampeningPower ) * orbitalDampeningStrength;

                        if ( Vector2.Dot ( forceAccumulator, transform.position - head.transform.position ) > 0 ) {
                            forceAccumulator -= (Vector2) Vector3.Project ( forceAccumulator, transform.position - head.transform.position );
                            forceAccumulator = Vector3.zero;
                        }

                        rgb.AddForce ( forceAccumulator, ForceMode2D.Impulse );
                        head.Propagate ( -forceAccumulator * rgb.mass );
                    }
                }
            }
            forceAccumulator = Vector3.zero;
        }
    }

    public override GameObject Fire ( Vector2 prv ) {
        headLaunched = true;
        head.transform.position = transform.position + transform.up * 2;
        head.transform.rotation = transform.rotation;
        head.gameObject.SetActive ( true );
        head.GetComponent<Rigidbody2D> ().velocity = (Vector3) prv + transform.up * launchSpeed;
        return null;
    }

    public override void TriggerRelease () {
        head.ResetParent ();
        head.gameObject.SetActive ( false );
        headLaunched = false;
        lineConn.attachLength = 0;
        lineConn.enabled = false;

        base.TriggerRelease ();
    }

    private void OnDisable () {
        TriggerRelease ();
    }
}
