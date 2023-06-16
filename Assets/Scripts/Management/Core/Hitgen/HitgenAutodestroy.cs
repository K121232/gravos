using UnityEngine;

public class HitgenAutodestroy : Hitgen {
    public GameObject   inactiveTarget;

    public override int Bump ( GameObject who = null, Vector2? deltaV = null ) {
        int delta = base.Bump();
        if ( inactiveTarget == null ) {
            inactiveTarget = gameObject;
        }
        inactiveTarget.SetActive ( false );
        return delta;
    }
}
