using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadoutMenu : MenuCore {
    public  InventoryMenu   ivm;

    public  List<ItemPort>      store;
    public  List<Multihelper>   helpers;

    public  Transform           hardpointsRoot;
    public  Multihelper[]       iHandles;

    public  Button              swapButton;

    public  ItemPort portA, portB;

    public override void Start () {
        base.Start ();
    }

    public void InitLeftSide ( ItemPort a ) {
        LoadInspect ( 0, a.item.GetTagName (), a );
    }
    public void InitCallbacks () {
        for ( int i = 0; i < helpers.Count; i++ ) {
            helpers [ i ].Init ( i );
            helpers [ i ].SetCallback ( 0, DisplayCallback );
        }
    }

    private bool CheckSwapPossibility () {
        return portA != null && portB != null && portB.Compatible ( portA );
    }

    public void Draw () {
        for ( int i = 0; i < helpers.Count; i++ ) {
            if ( CheckID ( i, store ) && store [ i ].item != null ) {
                helpers [ i ].anchors [ 0 ].GetComponent<Image> ().sprite = store [ i ].item.insignia;
            } else {
                helpers [ i ].anchors [ 0 ].GetComponent<Image> ().sprite = null;
            }
        }
        swapButton.interactable = CheckSwapPossibility ();
    }

    private bool CheckID<T> ( int a, List<T> b ) {
        return a >= 0 && a < b.Count;
    }

    public void DisplayCallback ( int id ) {
        if ( CheckID ( id, store ) && helpers [ id ] != null ) {
            LoadInspect ( 1, store [ id ].GetTagName () + ( id + 1 ).ToString (), store [ id ] );
        } else {
            LoadInspect ( 1 );
        }
        Draw ();
    }
    public  void    LoadInspect ( int target = 0, string pos = "X", ItemPort a = null ) {
        iHandles [ target ].SetLabel ( 0, pos );
        ItemHandle deltaIH = null;
        if ( a != null ) {
            deltaIH = a.item;
            if ( target == 0 ) portA = a; else portB = a;
        }
        if ( deltaIH != null ) {
            iHandles [ target ].SetLabel ( 1, deltaIH.itemName );
            iHandles [ target ].SetLabel ( 2, deltaIH.description );
            iHandles [ target ].anchors [ 0 ].GetComponent<Image> ().sprite = deltaIH.insignia;
        } else {
            iHandles [ target ].SetLabel ( 1, " \\ NOTHING // " );
            iHandles [ target ].SetLabel ( 2, ">_" );
            iHandles [ target ].anchors [ 0 ].GetComponent<Image> ().sprite = null;
        }
    }

    public  void    InitiateSwap () {
        if ( CheckSwapPossibility() ) {
            manager.RequestDecision ( SwapOptionCallback, "CANCEL", "SWAP" );
        }
    }

    public  void    SwapOptionCallback ( int a ) {
        if ( a == 1 ) {
            portB.Swap ( portA );
            ivm.Backflow ( true, 99 );
        }
    }
    
    public override void Incoming ( bool a ) {
        if ( a ) {
            portB = null;
            InitCallbacks ();
            Draw ();
            LoadInspect ( 1 );
            if ( helpers.Count > 0 ) {
                helpers [ 0 ].GetComponent<Button> ().Select ();
            }
        }
        base.Incoming ( a );
    }

    public override void Update () {
        base.Update ();
    }
}
