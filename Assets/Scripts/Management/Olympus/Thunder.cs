using UnityEngine;

public class Thunder : MonoBehaviour {
    public  Transform   target;
    private Rigidbody2D targetRGB;

    public  bool        autofire = true;
    public  Vector2     aimOffset = Vector2.up;

    private Rigidbody2D rgb;

    private FCM         fcm;
    private AIM         aim;
    private TFC         tfc;

    private void Start () {
        Autoload ();
        Autobind ();
        SetAutofire ( autofire );
    }

    private void Autoload () {
        fcm = GetComponent<FCM> ();
        aim = GetComponent<AIM> ();
        tfc = GetComponent<TFC> ();
    }

    private void Autobind () {
        if ( fcm != null ) {
            fcm.fireable = GetComponent<Fireable>();
        }
        if ( aim != null ) {
            aim.controller = this;
        }
        if ( tfc != null ) {
            tfc.controller = this;
        }
        Fireable fireable;
        if ( TryGetComponent ( out fireable ) ) {
            fireable.controller = this;
        }
    }

    private void MainBreaker ( bool status ) {
        Autoload ();
        if ( fcm != null ) fcm.enabled = status;
        if ( aim != null ) aim.enabled = status;
        if ( tfc != null ) tfc.enabled = status;
    }

    public void MainInit ( ItemPort host ) {
        Autoload ();
        rgb = null;
        if ( host != null && host.bungholio ) { // means we are live
            rgb = host.hullLink.GetComponent<Rigidbody2D> ();
            MainBreaker ( true );
        } else {
            MainBreaker ( false );
        }
        SetTarget ( null );
    }

    public void ForceFire ( bool alpha ) {
        if ( fcm != null ) {
            if ( alpha ) {
                fcm.TriggerHold ();
            } else {
                fcm.TriggerRelease ();
            }
        }
    }

    public void SetTarget ( Transform alpha ) {
        target = alpha;
        Collider2D  deltaC;
        if ( target != null && target.TryGetComponent ( out deltaC ) ) {
            targetRGB = deltaC.attachedRigidbody;
        } else {
            targetRGB = null;
        }
    }

    public void SetAutofire ( bool alpha ) {
        autofire = alpha;
        if ( tfc != null ) {
            tfc.enabled = alpha;
        }
    }

    public Vector2 GetV () {
        if ( rgb == null ) return Vector2.zero; return rgb.velocity;
    }

    public Vector2 GetVTarget () {
        if ( targetRGB == null ) return Vector2.zero; return targetRGB.velocity;
    }

    public float GetAIMDeviation () {
        if ( aim != null ) {
            return aim.GetLastDeviation ();
        }
        return 0;
    }

    // Get AIM Time To Target
    public float GetAIMTTT () {
        if ( aim != null && aim.traversalSpeed != 0 ) {
            return Mathf.Abs ( aim.GetLastDeviation () ) / aim.traversalSpeed;
        }
        return 0;
    }

    public float GetAngV () {
        if ( rgb == null ) return 0; return rgb.angularVelocity;
    }

    public float GetFireProgress () {
        float deltaTD = tfc == null ? 1 : tfc.GetProgress();
        float deltaFC = fcm == null ? 1 : fcm.GetProgress();
        return ( deltaFC + deltaTD ) / 2;
    }
}
