using UnityEngine;

public class ItemHandle : MonoBehaviour {
    public  string      itemName;
    public  string      description;
    public  int         itemQuantity;

    public  float       weight;
    public  bool        isWeapon;
    public  bool        isEquipment;

    public  Sprite      insignia;
    public virtual void Detach () {
        Debug.Log ( "Detach" );
    }

    public virtual void Attach () {
        Debug.Log ( "Attach" );
    }

    public virtual  bool Compatible ( ItemHandle right ) {
        return right.isWeapon == isWeapon && right.isEquipment == isEquipment;
    }
}
