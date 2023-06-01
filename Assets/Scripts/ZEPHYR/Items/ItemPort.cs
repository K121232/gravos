using UnityEngine;
using System;

public class ItemPort : ZephyrUnit {
    [Header("Item Port Base")]
    public  ItemPolarity    polarity;

    public  bool            bungholio = false;

    public  Transform       hullLink;
    public  Transform       batteryLink;

    public  Transform       specLink;

    public bool Compatible ( ItemPort other ) {
        if ( other.polarity == ItemPolarity.Item && other.mirror != null ) {
            return ItemPolarityChecker.CPC ( polarity, ( ( ItemHandle ) other.mirror ).polarity );
        }
        return ItemPolarityChecker.CPC ( polarity, other.polarity );
    }

    protected override void AutoloadCore () {
        mirror = transform.GetComponentInChildren<ItemHandle> (); ;
    }

    public override void Autobind ( ZephyrUnit _unit ) {
        base.Autobind ( _unit );

        if ( _unit != null ) {
            ItemHandle delta = _unit as ItemHandle;
            if ( bungholio && polarity == ItemPolarity.Weapon && delta.polarity == ItemPolarity.Weapon ) {
                specLink.GetComponent<TargetControlP> ().RefreshTurretList ();
            }
        }
    }

    public  ItemHandle  GetItem () {
        if ( mirror == null ) return null;
        return ( ItemHandle ) mirror;
    }

    public void Swap ( ItemPort other ) {
        if ( Compatible ( other ) ) {
            ZephyrUnit  h1 = mirror, h2 = other.mirror;

            h1.Autobreak ();
            h2.Autobreak ();

            Autobind ( h2 );
            other.Autobind ( h1 );
        }   
    }
}
