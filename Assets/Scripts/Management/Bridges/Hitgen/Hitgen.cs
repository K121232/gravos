using UnityEngine;

public class Hitgen : MonoBehaviour {
    public  int     damageBase;

    public  virtual int    Bump () {
        return damageBase;
    }
    
}
