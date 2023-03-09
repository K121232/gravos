using UnityEngine;

public class HSTM : TM {
    public  Transform   target;
    public  Rigidbody2D targetRGB;

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
        rgb     = null;
        target  = null;
    }

    public void Bind ( Transform alpha ) {
        target = alpha;
        alpha.TryGetComponent ( out targetRGB );
    }

    public override void Update () {
        targetLink = ( target.position - transform.position ) * STRP;
        targetLink -= rgb.velocity * STRV1;

        if ( targetRGB != null ) targetLink += targetRGB.velocity * STRV2;
        base.Update ();
    }
}
