using UnityEngine;

public class LaserFireable : PayloadFireable, INTFCM {
    public  float   range = Mathf.Infinity;
    public  int     penValue;       // Penetration count

    public  int     rayDamage;

    public  PoolSpooler poolLaserTrail;
    public  float       laserTrailTime;

    private RaycastHit2D[]  delta;
    private Hitbox          dhb;
    private TrailRenderer   tr;
    private GameObject      deltaOBJ;

    public override GameObject Fire () {
        delta = Physics2D.RaycastAll ( transform.position, transform.up, range, ~LayerMask.GetMask ( LayerMask.LayerToName ( gameObject.layer ) ) );
        for ( int i = 0; i < penValue + 1 && i < delta.Length; i++ ) {
            if ( delta [ i ].collider.TryGetComponent ( out dhb ) ) {
                if ( rayDamage > 0 ) {
                    dhb.DeltaF ( rayDamage );
                }

                deltaOBJ = poolLaserTrail.Request ();
                if ( deltaOBJ.TryGetComponent ( out tr ) ) {
                    tr.Clear ();
                    tr.AddPosition ( transform.position );
                    tr.AddPosition ( delta [ i ].point );
                    deltaOBJ.GetComponent<TrailAddon> ().lifespan = laserTrailTime;
                    deltaOBJ.SetActive ( true );
                }
                
                deltaOBJ = base.Fire ();
                deltaOBJ.transform.position = delta [ i ].point;
            }
        }
        return null;
    }
}
