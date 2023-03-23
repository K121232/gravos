using UnityEngine;

public class HitgenAutodestroy : Hitgen {
    public override int Bump ( GameObject who = null ) {
        gameObject.SetActive ( false );
        return base.Bump ();
    }
}
