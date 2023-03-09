using UnityEngine;

public class Turret : TriggerAssembly {
    public  GameObject  mainHull;
    public  PoolSpooler autoloader;
    public  PoolSpooler trailLoader;

    public  Transform   target;
    public  Rigidbody2D targetRGB;

    public  float       traversalSpeed;
    public  float       velocityPredictStrength;

    public  float       coneMaxDeviation;

    public  float       minRangeOffset;

    private Vector3     offset = Vector3.zero;
    private SOD         sod;
    public  bool        regen;
    public  float       f;
    public  float       z;
    public  float       r;

    public  bool        fireControlOverride = false;

    public  void LoadTarget ( GameObject alpha ) {
        if ( target == alpha ) return;
        if ( alpha == null ) { target = null; targetRGB = null; return; }
        target = alpha.transform;
        if ( !alpha.TryGetComponent ( out targetRGB ) ) {
            targetRGB = null;
        }
    }

    private void DEBUGRange( float tg ) {
        Vector3 hand = Vector3.up * tg;
        int segments = 30;
        for ( int i = 0; i < segments; i++ ) {
            Debug.DrawLine( transform.position + hand - offset, transform.position + Quaternion.Euler( 0, 0, 360 / segments ) * hand - offset );
            hand = Quaternion.Euler( 0, 0, 360 / segments ) * hand;
        }
    }

    public override void Start () {
        regen = true;
        if ( rgb == null ) {
            rgb = mainHull.GetComponent<Rigidbody2D> ();
        }
        base.Start ();
    }

    public override void Update() {
        Vector3 tgv;
        if ( target == null ) { tgv = transform.parent.up; } else { tgv = target.position - transform.position; }
        if ( rgb != null ) {
            tgv += (Vector3)rgb.velocity * velocityPredictStrength;
        }
        if ( targetRGB != null ) {
            tgv -= ( Vector3 )targetRGB.velocity * velocityPredictStrength;
        }
        float delta = Vector2.SignedAngle ( transform.up, tgv );
        float actualAngle = delta;
        if ( traversalSpeed != 0 ) {
            if ( regen ) {
                sod = new SOD ( f, z, r, delta );
                regen = false;
            }
            delta = sod.Update ( Time.deltaTime, delta, Vector2.SignedAngle ( Vector2.up, tgv ) );
            delta = Mathf.Clamp ( delta, -traversalSpeed, traversalSpeed ) * Time.deltaTime;
            transform.Rotate ( Vector3.forward, delta );
            //sod.UpdateYD ( delta );
        }
        //Debug.DrawLine( transform.position, transform.position + transform.up * 5, Color.cyan );
        //DEBUGRange( coneMaxRange );
       
        if ( deltaA == 0 ) { Reload(); }
        if ( !fireControlOverride ) {
            if ( target != null && Mathf.Abs ( actualAngle ) < coneMaxDeviation ) {
                TriggerHold ();
            } else {
                TriggerRelease ();
            }
        }

        base.Update();
    }
    public override void Fire( Vector2 prv ) {
        GameObject instPayload  = autoloader.Request();
        GameObject instTrail    = trailLoader.Request();
        instPayload.layer = gameObject.layer;

        instTrail.GetComponent<TrailAddon> ().Bind ( instPayload.transform );
        Vector3 minHullClearance = transform.position + transform.up * minRangeOffset;

        if ( instPayload.GetComponent<InertialImpactor> () != null ) {
            instPayload.GetComponent<InertialImpactor> ().Prime ( rgb );
        }

        if ( instPayload.GetComponent<HSTM>() != null ) {
            instPayload.GetComponent<HSTM> ().Bind ( target );
        }

        instPayload.transform.SetPositionAndRotation( minHullClearance, transform.rotation );

        instPayload.SetActive( true );
        instTrail.SetActive (true);
    }
}
