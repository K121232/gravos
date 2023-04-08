using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MenuCore {
    public  LoadoutMenu         ldm;

    public  List<ItemPort>      stores;

    public  int                 contextId = -1;
    public  int                 swapContextId = -1;

    public  Transform           listRoot;
    public  GameObject          listItemPrefab;

    public  TMP_Text            contextTitle;
    public  TMP_Text            contextDescription;

    public  GameObject          contextButtonRoot;
    public  GameObject          contextEquipButton;

    public  int     decisionType = 0;

    public void Redraw () {
        GameObject delta;
        for ( int i = 0; i < stores.Count; i++ ) {
                if ( i >= listRoot.childCount ) {
                    delta = Instantiate ( listItemPrefab, listRoot );
                    delta.GetComponent<RectTransform> ().Translate ( 0, -6 * i, 0, Space.Self );
                }
                // TESTING FOR ONCLICK LISTENERS
                listRoot.GetChild ( i ).GetComponent<Multihelper> ().Init ( i );
                listRoot.GetChild ( i ).GetComponent<Multihelper> ().SetCallback ( 0, ContextButtonOpenInfo );

            string labelN = "NO ITEM", labelQ = "X";
            if ( stores [ i ].item != null ) {
                labelN = stores [ i ].item.itemName;
                labelQ = stores [ i ].item.itemQuantity.ToString ();
            }
            listRoot.GetChild ( i ).GetComponent<Multihelper> ().SetLabel ( 0, labelN );
            listRoot.GetChild ( i ).GetComponent<Multihelper> ().SetLabel ( 1, labelQ );
        }
        if ( stores.Count > 0 ) {
            listRoot.GetChild ( 0 ).GetChild ( 1 ).GetComponent<Button> ().Select ();
        }
    }

    // CONTEXT BUTTON

    public void ContextButton ( int buttonType ) {
        swapContextId = -1;
        if ( contextId == -1 ) return;
        switch ( buttonType ) {
            case 0:             // DISCARD
                // Spawn holder object, and jettison in a random direction
                manager.RequestDecision ( DecisionCallback, "KEEP", "DISCARD" );
                break;
            case 1:             // EQUIP
                // Pull up the equip menu
                ContextButtonInitiateSwap ();
                break;
        }
    }

    public void ContextButtonOpenInfo ( int id ) {
        manager.CancelDecision ();
        if ( id < 0 || id > stores.Count || stores [ id ].item == null ) {
            contextButtonRoot.SetActive ( false );
            contextId = -1;
            contextTitle.text = "NOTHING SELECTED";
            contextDescription.text = " ┌───┐ \n │    X    │ \n └───┘";
        } else {
            contextButtonRoot.SetActive ( true );
            contextId = id;
            contextTitle.text = stores [ id ].item.itemName;
            contextDescription.text = stores [ id ].item.description;

            contextEquipButton.SetActive ( stores [ id ].item.polarity != ItemPolarity.Item );
            contextButtonRoot.transform.GetChild ( 0 ).GetComponent<Button> ().Select ();
        }
    }

    public void ContextButtonInitiateSwap () {
        if ( contextId != -1 ) {
            swapContextId = contextId;
            ldm.InitLeftSide ( stores [ contextId ] );
            ldm.Backflow ( true );
        }
    }
    
    // CALLBACKS 

    public void DecisionCallback ( int outcome ) {
        if ( contextId == -1 ) return;
        // Depends on what kind of decision was called
        // 0 is for discard
        switch ( decisionType ) {
            case 0:
                DiscardCallback ( outcome );
                break;
        }
        Redraw ();
    }

    public void DiscardCallback ( int a ) {

    }

    public void SwapCallback ( ItemPort other, int cid ) {
        if ( other != null && swapContextId != -1 ) {
            Debug.Log ( swapContextId + " " + other.name );

        }
        contextId = -1;
    }

    // OVERRIDES

    public override void Incoming ( bool a ) {
        if ( a ) {
            ContextButtonOpenInfo ( -1 );     // Clear the context window
            Redraw ();
        } else {
            ContextButtonOpenInfo ( -1 );
        }
        base.Incoming ( a );
    }

    public override void Update () {
        if ( Input.GetKeyDown ( KeyCode.P ) ) {
            Backflow ( !status );
        }
        base.Update ();
    }
}
