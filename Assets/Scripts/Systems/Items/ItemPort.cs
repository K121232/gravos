using UnityEngine;
using System;

public class ItemPort : ZephyrUnit {
    [Header("Item Port Base")]
    public  ItemPolarity    polarity;

    public  bool            bungholio = false;

    public  Transform       hullLink;
    public  Transform       batteryLink;

    public bool Compatible ( ItemPort other ) {
        if ( other.polarity == ItemPolarity.Item && other.mirror != null ) {
            return ItemPolarityChecker.CPC ( polarity, ( ( ItemHandle ) other.mirror ).polarity );
        }
        return ItemPolarityChecker.CPC ( polarity, other.polarity );
    }

    protected override void AutoloadCore () {
        mirror = transform.GetComponentInChildren<ItemHandle> (); ;
    }

    public  ItemHandle  GetItem () {
        if ( mirror == null ) return null;
        return ( ItemHandle ) mirror;
    }

    public void Swap ( ItemPort other ) {

    }
}
