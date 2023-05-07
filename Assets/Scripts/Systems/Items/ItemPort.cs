using UnityEngine;
using System;

public class ItemPort : ZephyrUnit {
    [Header("Item Port Base")]
    public  ItemHandle      item;

    public  ItemPolarity    polarity;

    public  bool            bungholio = false;

    public  Transform       hullLink;
    public  Transform       batteryLink;

    public string GetTagName () {
        if ( item == null ) return "X";
        return item.GetTagName ();
    }

    public bool CheckPolarities ( ItemPolarity rx, ItemPolarity tx ) {
        if ( rx == ItemPolarity.Item ) return true;
        if ( rx == tx ) return true;
        return false;
    }

    public bool Compatible ( ItemPort providingPort ) {
        if ( providingPort.polarity == ItemPolarity.Item && providingPort.item != null ) {
            return CheckPolarities ( polarity, providingPort.item.polarity );
        }
        return CheckPolarities ( polarity, providingPort.polarity );
    }

    public override void Autobinding ( ZephyrUnit _unit ) {
        if ( _unit != null ) {
            item = ( ItemHandle ) _unit;
        } else {
            item = null;
        }
        base.Autobinding ( _unit );
    }

    public override void Autobreak () {
        base.Autobreak ();
        item = null;
    }

    protected override void AutoloadCore () {
        item = transform.GetComponentInChildren<ItemHandle> ();
        bind = item;
    }

    public void Swap ( ItemPort other ) {
        
    }
}
