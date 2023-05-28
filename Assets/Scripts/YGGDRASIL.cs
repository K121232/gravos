using System;
using UnityEngine;

public class DataLinkNTF : IComparable {
    public  string      content = " !!! ";
    public  float       timeout = 5;            // If timeout = -1 then it stays on
    public  int         prio = 0;
    public DataLinkNTF ( string _content = " !!! ", float _timeout = -1, int _prio = 0 ) {
        content = _content;
        timeout = _timeout;
        prio = _prio;
    }

    public int CompareTo ( object obj ) {
        DataLinkNTF other = obj as DataLinkNTF;
        if ( prio == other.prio ) return 0;
        if ( prio == -1 ) return -1;
        if ( other.prio == -1 ) return 1;
        if ( prio > other.prio ) return -1;
        return 1;
    }
}

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