
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