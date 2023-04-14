using UnityEngine;

public class Turret : MonoBehaviour {
    private Rigidbody2D rgb;

    public  GameObject  mainHull;
    public  PoolSpooler autoloader;
    public  PoolSpooler trailLoader;

    public  Transform   target;
    public  float       minRangeOffset;

    public  bool        inheritLayer;
    public FCM          fcm;

    public virtual void Start () {
        rgb = GetComponent<Rigidbody2D> ();
        if ( fcm == null ) {
            fcm = GetComponent<FCM> ();
        }
        if ( fcm != null ) {
            fcm.burialHelper = FireWrapper;
        }
    }

    protected Vector2 GetV () {
        if ( rgb != null ) return rgb.velocity; return Vector2.zero;
    }

    public  void    SetTarget ( Transform _target ) {
        target = _target;
    }

    public virtual GameObject Fire () {
        GameObject instPayload  = autoloader.Request();
        if ( inheritLayer ) {
            instPayload.layer = gameObject.layer;
        }

        instPayload.transform.SetPositionAndRotation ( transform.position + transform.up * minRangeOffset, transform.rotation );

        if ( instPayload.GetComponent<IMP> () != null ) {
            instPayload.GetComponent<IMP> ().Prime ( GetV () );
        }

        if ( instPayload.GetComponent<HSTM> () != null ) {
            instPayload.GetComponent<HSTM> ().Bind ( target );
        }

        if ( instPayload.GetComponent<TMFuse> () != null ) {
            instPayload.GetComponent<TMFuse> ().Bind ( GetV(), transform.up );
        }

        instPayload.SetActive ( true );

        if ( trailLoader != null ) {
            GameObject instTrail    = trailLoader.Request();
            instTrail.GetComponent<TrailAddon> ().Bind ( instPayload.transform );
            instTrail.SetActive ( true );
        }

        return instPayload;
    }

    public void FireWrapper () {
        Fire ();
    }
}
