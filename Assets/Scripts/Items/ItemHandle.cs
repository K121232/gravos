using UnityEngine;
using System;

public enum ItemPolarity { Item, Weapon, Equipment };

public class ItemHandle : MonoBehaviour {
    [Header("Item Handle Base")]
    public  Action<ItemPort>    onDeltaCallback;
    public  ItemPort    host;

    public  string      itemName;
    public  string      description;
    public  int         itemQuantity;

    public  float           weight;
    public  ItemPolarity    polarity;

    public  Sprite      insignia;

    public string GetTagName () {
        switch ( polarity ) {
            case ( ItemPolarity.Weapon ) : return "W";
            case ( ItemPolarity.Equipment ) : return "E";
            case ( ItemPolarity.Item ) : return "I";
            default: return "X";
        }
    }

    public virtual void Attach ( ItemPort target ) {
        if ( host != null && host.item == this ) host.item = null;

        host = target;
        host.item = this;

        transform.SetParent ( host.transform );

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if ( onDeltaCallback != null ) {
            onDeltaCallback ( host );
        }
        host.OnDelta ();
    }
}
