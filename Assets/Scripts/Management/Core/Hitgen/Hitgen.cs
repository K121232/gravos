using UnityEngine;

public class Hitgen : MonoBehaviour {
    public  int     damageBase;

    public  virtual int    Bump ( GameObject who = null, Vector2? deltaV = null ) {
        return damageBase;
    }
}
