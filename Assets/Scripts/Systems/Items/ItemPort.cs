using UnityEngine;
using System;

public class ItemPort : MonoBehaviour {
    public  Action      attachCallback;

    public  Transform   hullLink;
    public  Transform   batteryLink;

    public  ItemPolarity    polarity;

    public  ItemHandle  item;
    public  bool        bungholio = false;

    public string GetTagName () {
        if ( item == null ) return "X";
        return item.GetTagName ();
    }

    public void Start () {
        Autoload ();
    }

    public void Autoload () {
        if ( item == null && transform.childCount != 0 ) {
            transform.GetChild ( 0 ).TryGetComponent ( out item );
            item.Attach ( this );
        }
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

    public  void  Swap ( ItemPort other ) {
        if ( Compatible ( other ) && other.item != null && item != null ) {
            ItemHandle delta = item;
            other.item.Attach ( this );
            delta.Attach ( other );
        }
    }

    public void OnAttach () {
        if ( attachCallback != null ) {
            attachCallback ();
        }
    }
}
