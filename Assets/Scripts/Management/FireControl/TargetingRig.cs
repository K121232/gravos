using UnityEngine;

public class TargetingRig : MonoBehaviour {
    public  int         fireControlId = -1;

    private TriggerAssembly triggerControls;
    private Rigidbody2D rgb;    

    public  Transform   target;
    public  Rigidbody2D targetRGB;

    public  float       traversalSpeed;
    public  float       velocityPredictStrength;

    public  float       coneMaxDeviation;

    private SOD         sod;
    public  float       f;
    public  float       z;
    public  float       r;

    public  bool        fireControlOverride = false;

    public void Start () {
        if ( GetComponent<ItemHandle> () ) {
            GetComponent<ItemHandle> ().onDeltaCallback = MainInit;
            MainInit ( GetComponent<ItemHandle> ().host );
        }
        sod = new SOD ( f, z, r, 0 );
        triggerControls = GetComponent<TriggerAssembly> ();
        if ( triggerControls == null ) {
            fireControlOverride = true;
        }
    }

    public void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        enabled = port.bungholio;
        rgb = port.hullLink.GetComponent<Rigidbody2D> ();
    }

    public void LoadTarget ( GameObject alpha ) {
        if ( target == alpha ) return;
        if ( alpha == null ) { target = null; targetRGB = null; return; }
        target = alpha.transform;
        if ( target.GetComponent<Collider2D> () != null ) {
            targetRGB = target.GetComponent<Collider2D> ().attachedRigidbody;
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
        if ( traversalSpeed != 0 ) {
            delta = sod.Update ( Time.deltaTime, delta, Vector2.SignedAngle ( Vector2.up, tgv ), rgb != null ? rgb.angularVelocity : 0 );
            delta = Mathf.Clamp ( delta, -traversalSpeed, traversalSpeed ) * Time.deltaTime;
            transform.Rotate ( Vector3.forward, delta );
        }

        if ( !fireControlOverride ) {
            if ( target != null && ( coneMaxDeviation >= 180 || Vector2.SignedAngle ( transform.up, tgv ) <= coneMaxDeviation ) ) {
                if ( triggerControls.GetType () == typeof ( Turret ) ) {
                    ( (Turret) triggerControls ).SetTarget ( target );
                }
                triggerControls.TriggerHold ();
            } else {
                triggerControls.TriggerRelease ();
            }
        }
    }
}