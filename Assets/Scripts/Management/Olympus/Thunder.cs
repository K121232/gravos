using UnityEngine;

public class Thunder : MonoBehaviour {
    public  Transform   target;
    private Rigidbody2D targetRGB;

    private Rigidbody2D rgb;

    private ItemHandle  handle;
    private FCM         fcm;
    private AIM         aim;
    private TFC         tfc;

    public  bool        useFCM = true;
    public  bool        useAIM = true;
    public  bool        useTFC = true;

    public  Vector2     aimOffset = Vector2.up;

    public  bool IsFiring () {
        if ( fcm == null ) return false;
        return fcm.isFiring;
    }

    private void Start () {
        Autoload ();
        Autobind ();
        SetAutofire ( useTFC );
    }

    private void Autoload () {
        handle = GetComponent<ItemHandle> ();
        if ( useFCM ) {
            fcm = GetComponent<FCM> ();
        } else {
            fcm = null;
        }
        if ( useAIM ) {
            aim = GetComponent<AIM> ();
        } else {
            aim = null;
        }
        if ( useTFC ) {
            tfc = GetComponent<TFC> ();
        } else {
            tfc = null;
        }
    }

    private void Autobind () {
        if ( aim != null ) {
            aim.SetController ( this );
        }
        if ( tfc != null ) {
            tfc.SetController ( this );
        }
        FireableCore fireable;
        if ( TryGetComponent ( out fireable ) ) {
            fireable.MainInit ( this );
        }
        if ( fcm != null ) {
            fcm.fireable = fireable;
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
            handle.SetVisuals ( true );
        } else {
            MainBreaker ( false );
            handle.SetVisuals ( false );
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
        if ( aim != null ) { aim.ResetPD (); }
        if ( target != null && target.TryGetComponent ( out deltaC ) ) {
            targetRGB = deltaC.attachedRigidbody;
        } else {
            targetRGB = null;
        }
    }

    public void SetAutofire ( bool alpha ) {
        useTFC = alpha;
        if ( tfc != null ) {
            tfc.enabled = alpha;
        }
        Autoload ();
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

    public float    GetTFCP () {
        return tfc == null ? 1 : tfc.GetProgress();
    }

    public float GetFireProgress () {
        float deltaTD = tfc == null ? 1 : tfc.GetProgress();
        float deltaFC = fcm == null ? 1 : fcm.GetProgress();
        return ( deltaFC + deltaTD ) / 2;
    }
}
