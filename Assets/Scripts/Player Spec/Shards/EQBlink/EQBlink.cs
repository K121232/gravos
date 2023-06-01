using UnityEngine;

public class EQBlink : EQBase {
    [Header ( "EQ Blink" )]
    
    public  float   distance;
    public  int     damage;

    public override void MainInit ( ItemPort port ) {
        base.MainInit ( port );
        canAuto = false;
    }

    public override void OnStartFire () {
        Collider2D[] delta = Physics2D.OverlapBoxAll ( transform.position + transform.up * distance / 2, new Vector2 ( 1, distance ),
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
            rgb.position = transform.position + transform.up * distance;
            for ( int i = 0; i < delta.Length; i++ ) {
                if ( delta [ i ].TryGetComponent ( out dhb ) ) {
                    dhb.DeltaF ( damage );
                }
            }
        }
    }

    public override void Fire () {
        base.Fire ();
    }
}
