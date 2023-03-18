using UnityEngine;

public class HSTM : TM {
    public  Transform   target;
    private Rigidbody2D targetRGB;

    public  float       STRP;
    public  float       STRV1;
    public  float       STRV2;

    private void OnEnable () {
        regen = true;
        if ( target != null && rgb == null ) {
            target.TryGetComponent<Rigidbody2D> ( out targetRGB );
        }
    }

    private void OnDisable () {
        target      = null;
        targetRGB   = null;
    }

    public void Bind ( Transform alpha ) {
        target = alpha;
        alpha.TryGetComponent ( out targetRGB );
    }

    public override void Update () {
        if ( target != null ) {
            targetLink = target.position * STRP;
            targetLink += (Vector2) transform.position * ( 1f - STRP );
            targetLink -= rgb.velocity * STRV1;

            if ( targetRGB != null ) targetLink += targetRGB.velocity * STRV2;
        } else {
            targetLink = transform.position + transform.up;
        }
        base.Update ();
    }
}
