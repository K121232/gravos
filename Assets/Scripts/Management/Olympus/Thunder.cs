using UnityEngine;

public class Thunder : MonoBehaviour {
    public  Transform   target;
    public  Rigidbody2D targetRGB;

    public  Rigidbody2D rgb;

    private ItemHandle  handle;
    private FCM         fcm;
    private AIMREAL         aim;
    private TFC         tfc;

    public  bool        useFCM = true;
    public  bool        useAIM = true;
    public  bool        useTFC = true;

    public  Vector2     aimOffset = Vector2.up;

    public bool IsFiring () {
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
            aim = GetComponent<AIMREAL> ();
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
        UnifiedOrdnance uo;
        if ( TryGetComponent ( out uo ) ) {
            uo.MainInit ( this );
        }
        if ( fcm != null ) {
            fcm.fireable = uo;
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
            Autobind ();
            if ( fcm != null && fcm is FCMPC ) {
                ( fcm as FCMPC ).LoadCell ( host.batteryLink );
            }
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

    public float GetAIMTTT () {
        if ( aim != null && aim.traversalSpeed != 0 ) {
            return Mathf.Abs ( aim.GetLastDeviation () ) / aim.traversalSpeed;
        }
        return 0;
    }

    public float GetAngV ( float turretAdditive = 0 ) {
        if ( rgb == null ) return turretAdditive; 
        return rgb.angularVelocity + turretAdditive;
    }

    public float GetTFCP () {
        return tfc == null ? 1 : tfc.GetProgress ();
    }
    public float GetFCMP () {
        return fcm == null ? 1 : fcm.GetProgress ();
    }

    public float GetFireProgress () {
        return ( GetTFCP () + GetFCMP () ) / 2;
    }
}
