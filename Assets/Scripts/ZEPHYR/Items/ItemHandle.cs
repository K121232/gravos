using UnityEngine;

public class ItemHandle : ZephyrUnit {
    [Header("Item Handle Base")]
    public  ItemPolarity    polarity;

    public  string      itemName;
    public  string      description;
    public  Sprite      icon;

    public  GameObject  visuals;

    public string GetTagName () {
        return ItemPolarityChecker.TFP ( polarity );
    }

    public virtual void SetVisuals ( bool alpha ) {
        if ( visuals != null ) {
            visuals.SetActive ( alpha );
        }
    }

    public override void Autobind ( ZephyrUnit _unit ) {
        ItemPort host = null;
        if ( _unit != null ) {
            host = ( ItemPort ) _unit;

            transform.SetParent ( host.transform );
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            SetVisuals ( true );
        } else {
            SetVisuals ( false );
        }

        Thunder thd;
        if ( TryGetComponent( out thd ) ) {
            thd.MainInit ( host );
        }

        base.Autobind ( _unit );
    }
}
