using UnityEngine;

public class MonoBlock : Hitbox {
    public  int         hitPointsBase;
    public  int         hitPointsCurrent;

    public void OnEnable() {
        hitPointsCurrent = hitPointsBase;
    }
    public override void DeltaF ( int a ) {
        hitPointsCurrent -= a;
        if ( hitPointsCurrent <= 0 ) {
            gameObject.SetActive ( false );
        }
    }
}
