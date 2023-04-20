using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public  ItemPort[]      ports;
    public  Radar           pickupRadar;

    public  bool            canPickup;

    // Get some way to display the option to pick up an item

    public void Update () {
        canPickup = false;
        if ( pickupRadar.collectedCount != 0 ) {
            for ( int i = 0; i < pickupRadar.collectedCount; i++ ) {
                if ( pickupRadar.collectedColliders [ i ].GetComponent<ItemPort> () != null ) {
                    canPickup = true;
                    if ( Input.GetKeyDown ( KeyCode.Q ) ) {
                        AddItem ( pickupRadar.collectedColliders [ i ].GetComponent<ItemPort> () );
                        pickupRadar.collectedColliders [ i ].gameObject.SetActive ( false );
                        canPickup = false;
                        return;
                    }
                }
            }
        }
    }

    public void AddItem ( ItemPort alpha ) {
        for ( int i = 0; i < ports.Length; i++ ) {
            if ( ports [ i ].item == null ) {
                alpha.item.Attach ( ports [ i ] );
                return;
            }
        }
    }
}
