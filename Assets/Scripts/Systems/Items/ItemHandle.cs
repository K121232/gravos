using UnityEngine;
using System;

public class ItemHandle : ZephyrUnit {
    [Header("Item Handle Base")]
    public  ItemPolarity    polarity;

    public  string      itemName;
    public  string      description;
    public  int         itemQuantity = 1;
    public  float       weight = 1;
    public  Sprite      icon;

    public string GetTagName () {
        return ItemPolarityChecker.TFP ( polarity );
    }

    public override void Autobinding ( ZephyrUnit _unit ) {
        ItemPort host = null;
        if ( _unit != null ) {
            host = ( ItemPort ) _unit;
        }

        EQBase  eqb;
        if ( polarity == ItemPolarity.Equipment && TryGetComponent ( out eqb ) ) {
            eqb.MainInit ( host );
        }
        TargetingRig tgr;
        if ( polarity == ItemPolarity.Weapon && TryGetComponent<TargetingRig>( out tgr ) ) {
            tgr.MainInit ( host );
        }

        base.Autobinding ( _unit );
    }

    public override void Seal () {
        base.Seal ();
        transform.SetParent ( bind.transform );
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

}
