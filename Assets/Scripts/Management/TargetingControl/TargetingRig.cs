using UnityEngine;

public class TargetingRig : MonoBehaviour {
    private FCM triggerControls;
    private Rigidbody2D rgb;    

    public  Transform   target;
    public  Rigidbody2D targetRGB;

    public  float       traversalSpeed;
    public  float       velocityPredictStrength;

    public  float       coneMaxDeviation;

    private Konig         sod;

    public  bool        fireControlOverride = false;
    public  float       pastDeviation;

    public void Start () {
        sod = GetComponent<Konig> ();
        if ( GetComponent<ItemHandle> () ) {
            MainInit ( GetComponent<ItemHandle> ().host );
        }
        triggerControls = GetComponent<FCM> ();
        if ( triggerControls == null ) {
            fireControlOverride = true;
        }
    }

    public void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        enabled = port.bungholio;
        rgb = port.hullLink.GetComponent<Rigidbody2D> ();
    }

    public void LoadTarget ( Transform alpha ) {
        if ( alpha == target) return;
        target = alpha;
        if ( target != null && target.GetComponent<Collider2D> () != null ) {
            targetRGB = target.GetComponent<Collider2D> ().attachedRigidbody;
        } else {
            targetRGB = null;
        }
    }

    public  void    OverrideTriggerPress ( bool _press ) {
        if ( triggerControls != null ) {
            if ( _press ) {
                triggerControls.TriggerHold ();
            } else {
                triggerControls.TriggerRelease ();
            }
        }
    }

    private void Update () {
        Vector3 tgv;

        if ( target == null ) { tgv = transform.parent.up; } else {
            tgv = target.position - transform.position;
            if ( rgb != null ) {
                tgv -= (Vector3) rgb.velocity * velocityPredictStrength;
            }
            if ( targetRGB != null ) {
                tgv += (Vector3) targetRGB.velocity * velocityPredictStrength;
            }
        }

        float delta = Vector2.SignedAngle ( transform.up, tgv );
        pastDeviation = delta;
        if ( traversalSpeed != 0 ) {
            delta = sod.NextFrame ( Time.deltaTime, delta, Vector2.SignedAngle ( Vector2.up, tgv ), rgb != null ? rgb.angularVelocity : 0 );
            delta = Mathf.Clamp ( delta, -traversalSpeed, traversalSpeed ) * Time.deltaTime;
            transform.Rotate ( Vector3.forward, delta );
        }

        if ( !fireControlOverride ) {
            if ( target != null && ( coneMaxDeviation >= 180 || Vector2.Angle ( transform.up, tgv ) <= coneMaxDeviation ) ) {
                if ( GetComponent<Turret>() != null ) {
                    GetComponent<Turret> ().SetTarget ( target );
                }
                triggerControls.TriggerHold ();
            } else {
                triggerControls.TriggerRelease ();
            }
        }
    }

    public  float   GetFiringProgress () {
        float deltaTD = 1;
        if ( traversalSpeed != 0 && coneMaxDeviation != 0 ) {
            if ( Mathf.Abs ( pastDeviation ) < coneMaxDeviation ) {
                deltaTD = 1;
            } else {
                deltaTD = Mathf.Clamp ( 2 - ( Mathf.Abs ( pastDeviation ) / traversalSpeed ), 0, 1 );
            }
        }
        float deltaFC = triggerControls.GetCooldownProgress();
        if ( triggerControls.fireRate < 0.25f ) deltaFC = 1;
        return ( deltaFC + deltaTD ) / 2;
    }
}