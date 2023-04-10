using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public  ItemPort[]      ports;
    public  Radar           pickupRadar;

    // Get some way to display the option to pick up an item

    public void Update () {
        if ( pickupRadar.collectedCount != 0 && Input.GetKeyDown ( KeyCode.Q ) ) {
            for ( int i = 0; i < pickupRadar.collectedCount; i++ ) {
                if ( pickupRadar.collectedColliders[ i ].GetComponent<ItemPort>() != null ) {
                    AddItem ( pickupRadar.collectedColliders [ i ].GetComponent<ItemPort> () );
                    pickupRadar.collectedColliders [ i ].gameObject.SetActive ( false );
                    return;
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
