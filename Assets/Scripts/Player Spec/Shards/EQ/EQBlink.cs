using UnityEngine;

public class EQBlink : UnifiedOrdnance {
    [Header ( "EQ Blink" )]
    private Rigidbody2D rgb;

    public  float   distance;
    public  int     damage;

    public override void MainInit ( Thunder _controller ) {
        base.MainInit ( _controller );
        if ( enabled && controller != null ) {
            rgb = controller.rgb;
        } else {
            rgb = null;
        }
    }

    public override void OnStartFire () {
        if ( controller == null ) return;
        Collider2D[] delta = Physics2D.OverlapBoxAll ( rgb.transform.position + transform.up * distance / 2, new Vector2 ( 1, distance ),
            transform.eulerAngles.z, ~LayerMask.GetMask ( LayerMask.LayerToName ( gameObject.layer ) )
            );
        Hitbox dhb;
        bool canBlink = true;
        for ( int i = 0; i < delta.Length; i++ ) {
            if ( !delta [ i ].isTrigger && ( !delta [ i ].TryGetComponent ( out dhb ) || delta [ i ].GetComponent<Hitgen> () == null ) ) {
                canBlink = false;
                break;
            }
        }
        if ( canBlink ) {
            rgb.position = rgb.transform.position + transform.up * distance;
            for ( int i = 0; i < delta.Length; i++ ) {
                if ( delta [ i ].TryGetComponent ( out dhb ) ) {
                    dhb.DeltaF ( damage );
                }
            }
        }
    }
}
