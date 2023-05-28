using UnityEngine;

public class ShieldHitbox : Hitbox {
    public  int baseHP;
    private int deltaHP;

    private void OnEnable () {
        deltaHP = baseHP;
    }

    public override void DeltaF ( int a ) {
        deltaHP -= a;
        if ( deltaHP < 0 ) {
            gameObject.SetActive ( false );
        }
    }
}
