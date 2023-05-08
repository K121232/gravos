using UnityEngine;
using System;

public class ItemPort : ZephyrUnit {
    [Header("Item Port Base")]
    public  ItemPolarity    polarity;

    public  bool            bungholio = false;

    public  Transform       hullLink;
    public  Transform       batteryLink;

    public bool Compatible ( ItemPort other ) {
        if ( other.polarity == ItemPolarity.Item && other.bind != null ) {
            return ItemPolarityChecker.CPC ( polarity, ( ( ItemHandle ) other.bind ).polarity );
        }
        return ItemPolarityChecker.CPC ( polarity, other.polarity );
    }

    protected override void AutoloadCore () {
        bind = transform.GetComponentInChildren<ItemHandle> (); ;
    }

    public  ItemHandle  GetItem () {
        if ( bind == null ) return null;
        return ( ItemHandle ) bind;
    }

    public void Swap ( ItemPort other ) {

    }
}
