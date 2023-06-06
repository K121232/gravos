using UnityEngine;

public class HitgenAutodestroy : Hitgen {
    public override int Bump ( GameObject who = null, Vector2? deltaV = null ) {
        int delta = base.Bump();
        gameObject.SetActive ( false );
        return delta;
    }
}
