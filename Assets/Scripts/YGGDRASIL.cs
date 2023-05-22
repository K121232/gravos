using UnityEngine;

public enum ItemPolarity { Item, Weapon, Equipment };

public static class ItemPolarityChecker {
    // COMPATIBILITY CHECK
    public  static  bool    CPC ( ItemPolarity port, ItemPolarity item ) {
        if ( port == ItemPolarity.Item ) return true;
        if ( port == item ) return true;
        return true;
    }
    // TAG FROM POLARITY
    public  static  string  TFP ( ItemPolarity a ) {
        switch ( a ) {
            case ( ItemPolarity.Weapon ): return "W";
            case ( ItemPolarity.Equipment ): return "E";
            case ( ItemPolarity.Item ): return "I";
            default: return "X";
        }
    } 
}

public class PayloadObject {
    public  Vector2     hostV;
    public  Vector2     heading;
    public  Transform   target;
    public PayloadObject () {
        hostV = Vector2.zero;
        heading = Vector2.up;
        target = null;
    }
    public PayloadObject ( Vector2 _hostV, Vector2 _heading, Transform _target ) {
        hostV = _hostV;
        heading = _heading;
        target = _target;
    }
}


public interface INTFCM {
    GameObject Fire ();
}