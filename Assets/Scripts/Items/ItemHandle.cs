using UnityEngine;

public class ItemHandle : MonoBehaviour {
    public  string      itemName;
    public  string      description;
    
    public  float       weight;
    public  bool        isWeapon;
    
    public virtual void Detach () {
        Debug.Log ( "Detach" );
    }

    public virtual void Attach () {
        Debug.Log ( "Attach" );
    }
}
