using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public  ItemEjector     ejector;
    public  Radar           pickupRadar;
    public  int             fillCount = 0;

    public  bool            canPickup;

    public  void    RecalculateFill () {
        fillCount = 0;
        for ( int i = 0; i < ejector.ports.Length; i++ ) {
            if ( ejector.ports [ i ].GetItem () != null ) {
                fillCount++;
            }
        }
    }

    private void Start () {
        RecalculateFill ();
    }

    public void Update () {
        canPickup = false;
        int deltaI = 0;
        if ( pickupRadar.collectedCount != 0 && fillCount != ejector.ports.Length ) {
            for ( int i = 0; i < pickupRadar.collectedCount; i++ ) {
                if ( pickupRadar.collectedColliders [ i ].GetComponent<ItemPort> () != null ) {
                    canPickup = true;
                    deltaI = i;
                    break;
                    
                }
            }
        }
        if ( canPickup && Input.GetKeyDown ( KeyCode.Q ) ) {
            if ( AddItem ( pickupRadar.collectedColliders [ deltaI ].GetComponent<ItemPort> () ) ) {
                pickupRadar.collectedColliders [ deltaI ].gameObject.SetActive ( false );
            }
            RecalculateFill ();
            canPickup = false;
        }
    }

    public bool AddItem ( ItemPort alpha ) {
        for ( int i = 0; i < ejector.ports.Length; i++ ) {
            if ( ejector.ports [ i ].GetItem () == null ) {
                alpha.Autobinding ( ejector.ports [ i ] );
                return true;
            }
        }
        return false;
    }
}
