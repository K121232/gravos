using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryMenu : MenuCore {
    public  LoadoutMenu         ldm;

    public  List<ItemHandle>    stores;
    public  int                 contextId = -1;

    public  Transform           listRoot;
    public  GameObject          listItemPrefab;

    public  TMP_Text            contextTitle;
    public  TMP_Text            contextDescription;
    public  GameObject          contextButtonRoot;
    public  GameObject          contextEquipButton;

    public  void    ContextButton ( int buttonType ) {
        if ( contextId == -1 ) return;
        switch ( buttonType ) {
            case 0:             // DISCARD
                // Spawn holder object, and jettison in a random direction
                manager.RequestDecision ( DecisionCallback, "KEEP", "DISCARD" );
                break;
            case 1:             // EQUIP
                // Pull up the equip menu
                InitiateSwap ();
                break;
        }
    }

    public  void    PopulateList () {
        for ( int i = stores.Count; i < listRoot.childCount; i++ ) {
            Destroy ( listRoot.GetChild ( i ) );
        }
        GameObject delta;
        for ( int i = 0; i < stores.Count; i++ ) {
            if ( i >= listRoot.childCount ) {
                Debug.Log ( "Made new item" );
                delta = Instantiate ( listItemPrefab, listRoot );
                //delta.GetComponent<RectTransform> ().Translate ( 0, - 60 - 125 * i, 0 );
            }
            // TESTING FOR ONCLICK LISTENERS
            listRoot.GetChild ( i ).GetComponent<Multihelper> ().Init ( i );
            listRoot.GetChild ( i ).GetComponent<Multihelper> ().SetLabel ( 0, stores [ i ].name );
            listRoot.GetChild ( i ).GetComponent<Multihelper> ().SetLabel ( 1, stores [ i ].itemQuantity.ToString() );
            listRoot.GetChild ( i ).GetComponent<Multihelper> ().SetCallback ( 0, OpenContext );

        }
    }

    public  void    OpenContext ( int id ) {
        Debug.Log ( "OPEN CONTEXT ID : " + id );
        if ( id < 0 || id > stores.Count ) {
            contextId = -1;
            contextTitle.text = "NOTHING SELECTED";
            contextDescription.text = " ┌───┐ \n │    X    │ \n └───┘";
            contextButtonRoot.SetActive ( false );
            return;
        }
        contextButtonRoot.SetActive ( true );
        contextId = id;
        contextTitle.text       = stores [ id ].itemName;
        contextDescription.text = stores [ id ].description;
        // Enable buttons based on what the item can do
        contextEquipButton.SetActive ( stores [ id ].isWeapon );
    }

    public  int     decisionType = 0;
    public  void    DecisionCallback ( int outcome ) {
        if ( contextId == -1 ) return;
        // Depends on what kind of decision was called
        // 0 is for discard
        switch ( decisionType ) {
            case 0:
                if ( outcome == 1 ) {
                    Debug.Log ( "ITEM " + contextId + " HAS BEEN DISCARDED" );
                }
                if ( outcome == -1 ) {
                    Debug.Log ( "ITEM " + contextId + " HAS NOT BEEN DISCARDED" );
                }
                break;
        }
    }

    public  void    InitiateSwap () {
        if ( contextId != -1 ) {
            ldm.InitLeftSide ( stores [ contextId ] );
            ldm.Backflow ( true );
        }
    }

    public override void Incoming ( bool a ) {
        if ( a ) {
            OpenContext ( -1 );     // Clear the context window
            PopulateList ();
        } else {
            OpenContext ( -1 );
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
