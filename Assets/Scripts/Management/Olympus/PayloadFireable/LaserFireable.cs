using UnityEngine;

public class LaserFireable : PayloadFireable {
    public  float   range = Mathf.Infinity;
    public  int     penValue;       // Penetration count

    public  int     rayDamage;

    public  PoolSpooler poolLaserTrail;
    public  float       laserTrailTime;

    private RaycastHit2D[]  delta;
    private Hitbox          dhb;

    public override void Fire () {
        delta = Physics2D.RaycastAll ( transform.position, transform.up, range, ~LayerMask.GetMask ( LayerMask.LayerToName ( gameObject.layer ) ) );
        for ( int i = 0; i < penValue + 1 && i < delta.Length; i++ ) {
            if ( delta [ i ].collider.TryGetComponent ( out dhb ) ) {
                if ( rayDamage > 0 ) {
                    dhb.DeltaF ( rayDamage );
                }
                base.Fire ();
                if ( transfer != null ) {
                    transfer.transform.position = delta [ i ].point;
                }
            }
        }
    }
}
