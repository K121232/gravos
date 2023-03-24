using UnityEngine;

public class EQGrapple : TriggerAssembly {
    public  EQGrappleHook   hookLink;
    public  LineConnector   lineConn;
    public  ForceTether     lineTeth;

    public  AimHelper   aimHelper;
    public  Transform   aimHelperTarget;
    public  Transform   aimHelperBase;

    public  float       launchSpeed;
    public  float       maxRange;
    public  float       minAttachRange;
    public  bool        hookLaunched;


    public override void Start () {
        SetRGB ( transform.parent.parent.GetComponent<Rigidbody2D> () );

        hookLink.BaseInit ( transform, maxRange, this );
        lineConn.BaseInit ( transform, hookLink.transform );
        lineTeth.BaseInit ( rgb, rgb.transform );

        hookLaunched = true;
        
        base.Start ();
    }

    public override void Update () {
        if ( Input.GetAxis ( "Fire2" ) > 0 ) {
            TriggerHold ();
        } else {
            TriggerRelease ();
        }
        lineConn.enabled = hookLaunched && hookLink.isActiveAndEnabled;
        base.Update ();
    }

    public override GameObject Fire ( Vector2 prv ) {
        hookLaunched = true;
        hookLink.transform.position = transform.position + transform.up * 2;
        hookLink.transform.rotation = transform.rotation;
        hookLink.gameObject.SetActive ( true );
        hookLink.GetComponent<Rigidbody2D> ().velocity = (Vector3) prv + transform.up * launchSpeed;
        return null;
    }

    public override void TriggerRelease () {
        if ( hookLaunched ) {
            HookDetach ();
            aimHelper.LockIn ( aimHelperBase );
        }
        lineTeth.enabled = false;
        lineConn.enabled = false;
        base.TriggerRelease ();
    }

    private void OnDisable () {
        TriggerRelease ();
    }

    public void HookAttach ( Rigidbody2D objectHooked ) {
        hookLink.attachLength = Mathf.Max ( minAttachRange, hookLink.attachLength );
        
        lineConn.attachLength = hookLink.attachLength;
        lineTeth.enabled = true;

        lineTeth.LoadBL ( objectHooked, hookLink.transform, hookLink.attachLength );
        aimHelper.LockIn ( aimHelperTarget );
    }

    public void HookDetach ( bool hookDoReset = true ) {
        if ( hookDoReset ) {
            hookLink.ResetParent ();
            hookLink.gameObject.SetActive ( false );
        }

        hookLaunched = false;

        lineConn.enabled = false;
        lineTeth.enabled = false;
    }
}
