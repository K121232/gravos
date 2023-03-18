using UnityEngine;

public class Manifest : MonoBehaviour {
    public  string      displayName;

    public enum InfoType { CARGO, ARMAMENT, NAME };
    public  string      cargoInfo;
    public  string      armamentInfo;

    public  bool        broadcasting;

    private void Start () {
        if ( displayName == "" ) {
            displayName = gameObject.name;
        }
    }
    
    public  string  TryGetInfo ( InfoType a ) {
        if ( broadcasting ) {
            switch ( a ) {
                case InfoType.NAME:
                    return displayName;
                case InfoType.CARGO:
                    return cargoInfo;
                case InfoType.ARMAMENT:
                    return armamentInfo;
            }
        } 
        return "N/A";        
    }
}
