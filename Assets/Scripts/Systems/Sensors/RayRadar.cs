using UnityEngine;

public class RayRadar : Radar {
    public  float       angleOffset;
    public  float       range;
    public  int         rayCount;
    public  float       rayOffset;

    public  LayerMask   mask;
    private Vector2     lead;

    private Collider2D          targetCollider;
    private RaycastHit2D[]    deltaRH;

    public override void Start() {
        targetCollider = transform.GetComponent<Collider2D>();
        base.Start();
    }

    public override void Update() {
        Clear();

        if ( rayCount % 2 == 0 ) {
            lead = Quaternion.Euler( 0, 0, angleOffset - rayOffset * ( ( rayCount / 2 ) - 0.5f ) ) * transform.up;
        } else {
            lead = Quaternion.Euler( 0, 0, angleOffset - rayOffset * ( rayCount - 1 ) / 2 ) * transform.up;
        }

        for ( int i = 0; i < rayCount; i++ ) {
            targetCollider.Raycast( lead, deltaRH, range, mask );
            //Debug.DrawLine( transform.position, ( Vector2 ) transform.position + lead * range, Color.black );
            for ( int k = 0; k < deltaRH.Length; k++ ) {
                if ( deltaRH[k].collider != null ) {
                    collectedColliders.Add( deltaRH[k].collider );
                } else { break; }
            }
            lead = Quaternion.Euler( 0, 0, rayOffset ) * lead;
        }
    }

    public override void Clear() {
        deltaRH = new RaycastHit2D[16];
        base.Clear();
    }
}
