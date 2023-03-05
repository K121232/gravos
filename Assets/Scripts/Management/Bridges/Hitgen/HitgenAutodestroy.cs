using UnityEngine;

public class HitgenAutodestroy : Hitgen {
    public override int Bump () {
        gameObject.SetActive ( false );
        return base.Bump ();
    }
}
