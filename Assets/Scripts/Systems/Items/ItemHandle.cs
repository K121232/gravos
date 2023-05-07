using UnityEngine;
using System;

public enum ItemPolarity { Item, Weapon, Equipment };

public class ItemHandle : ZephyrUnit {
    [Header("Item Handle Base")]
    public  ItemPort    host;

    public  ItemPolarity    polarity;

    public  string      itemName;
    public  string      description;
    public  int         itemQuantity = 1;
    public  float       weight = 1;
    public  Sprite      icon;

    protected override void Start () {
        base.Start ();
    }

    public string GetTagName () {
        switch ( polarity ) {
            case ( ItemPolarity.Weapon ):       return "W";
            case ( ItemPolarity.Equipment ):    return "E";
            case ( ItemPolarity.Item ):         return "I";
            default: return "X";
        }
    }

    public override void Autobinding ( ZephyrUnit _unit ) {
        if ( _unit != null ) {
            host = ( ItemPort ) _unit;
        } else {
            host = null;
        }

        EQBase  eqb;
        if ( polarity == ItemPolarity.Equipment && TryGetComponent ( out eqb ) ) {
            eqb.MainInit ( host );
        }

        base.Autobinding ( _unit );
    }

    public override void Autobreak () {
        base.Autobreak ();
        host = null;
    }

    public override void Seal () {
        base.Seal ();
        transform.SetParent ( host.transform );
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

}
