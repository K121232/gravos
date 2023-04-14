using UnityEngine;

public class HitgenAutodestroy : Hitgen {
    public override int Bump ( GameObject who = null, Vector2? deltaV = null ) {
        gameObject.SetActive ( false );
        return base.Bump ();
    }
}
