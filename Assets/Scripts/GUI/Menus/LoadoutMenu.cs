using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadoutMenu : MenuCore {
    public  InventoryMenu   ivm;

    public  Transform       hardpointsRoot;

    public  Multihelper[]       iHandles;

    public  List<ItemHandle>    storeW;
    public  List<ItemHandle>    storeE;

    public  List<Multihelper>   helpersW;
    public  List<Multihelper>   helpersE;

    private ItemHandle          objA, objB;

    public  Button              swapButton;

    public override void Start () {
        //storeW = new List<ItemHandle> ();
        //storeE = new List<ItemHandle> ();
        objA = null;
        objB = null;
        base.Start ();
    }

    public  void    InitLeftSide ( ItemHandle a ) {
        LoadInspect ( a.isWeapon ? "W" : "E", a, 0 );
    }
    public  void    InitCallbacks () {
        for ( int i = 0; i < storeW.Count; i++ ) {
            helpersW [ i ].Init ( i );
            helpersW [ i ].SetCallback ( 0, DisplayCallback );
        }
    }
    public void Draw () {
        for ( int i = 0; i < storeW.Count; i++ ) {
            helpersW [ i ].anchors [ 0 ].GetComponent<Image> ().sprite = storeW [ i ].insignia;
        }
        swapButton.interactable = objA != null && objB != null && objA.Compatible ( objB );
    }

    public void DisplayCallback ( int id ) {
        if ( id == -1 ) {
            LoadInspect ( "X", null, 1 );
            return;
        }
        if ( id >= helpersW.Count ) {
            id -= helpersW.Count;
            LoadInspect ( "E" + ( id + 1 ).ToString (), storeE [ id ], 1 );
        } else {
            LoadInspect ( "W" + ( id + 1 ).ToString (), storeW [ id ], 1 );
        }
        Draw ();
    }
    public  void    LoadInspect ( string pos, ItemHandle a, int target ) {
        iHandles [ target ].SetLabel ( 0, pos );
        if ( a != null ) {
            iHandles [ target ].SetLabel ( 1, a.itemName );
            iHandles [ target ].SetLabel ( 2, a.description );
            iHandles [ target ].anchors [ 0 ].GetComponent<Image> ().sprite = a.insignia;
        } else {
            iHandles [ target ].SetLabel ( 1, " \\ NOTHING // " );
            iHandles [ target ].SetLabel ( 2, ">_" );
            // Have a default insignia sprite
            iHandles [ target ].anchors [ 0 ].GetComponent<Image> ().sprite = null;
        }
        if ( target == 0 ) {
            objA = a;
        } else {
            objB = a;
        }
    }

    public  void    InitiateSwap () {
        if ( objA != null && objB != null && objA.Compatible ( objB ) ) {
            manager.RequestDecision ( SwapOptionCallback, "CANCEL", "SWAP" );
        }
    }

    public  void    SwapOptionCallback ( int a ) {
        if ( a == 1 ) {
            Debug.Log ( "HERE WE SWAP" );
            ivm.Backflow ( true, 99 );
        }
    }
    

    public override void Incoming ( bool a ) {
        if ( !a ) {
            objA = null; objB = null;
        } else {
            InitCallbacks ();
            Draw ();
            LoadInspect ( "X", null, 1 );
        }
        base.Incoming ( a );
    }

    public override void Update () {
        base.Update ();
    }
}
