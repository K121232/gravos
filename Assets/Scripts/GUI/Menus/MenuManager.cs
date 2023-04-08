using UnityEngine;
using System;

public class MenuManager : MonoBehaviour {
    [System.Serializable]
    public struct MenuEntry {
        public  MenuCore    menuHandle;
        public  bool        status;
        public  int         prio;
        public  bool        pauses;
    }

    private int             currentPrio;
    public bool menuLock { get; private set; }
    public  MenuEntry[]     menus;

    public  OptionMenu      optionMenu;
    public  Action<int>     reqCallback;
    public  bool            inDecision;

    private void Start () {
        for ( int i = 0; i < menus.Length; i++ ) {
            menus [ i ].menuHandle.Handshake ( this, i );
            menus [ i ].menuHandle.Incoming ( false );
            menus [ i ].status = false;
        }
        currentPrio = -1;
    }

    private bool delta;

    public void MenuRequest ( int id, bool target, int specPrio = -1 ) {
        if ( inDecision ) { return; }
        if ( specPrio == -1 ) { specPrio = menus [ id ].prio; }
        if ( target && currentPrio > specPrio ) {
            return;
        }
        Time.timeScale = 1;
        currentPrio = -1;
        for ( int i = 0; i < menus.Length; i++ ) {
            delta = i == id ? target : ( !target ? menus [ i ].status : false );
            if ( delta != menus [ i ].status ) {
                menus [ i ].menuHandle.Incoming ( delta );
                menus [ i ].status = delta;
            }
            if ( menus [ i ].status ) {
                currentPrio = menus [ i ].prio;
                Time.timeScale = menus [ i ].pauses ? 0 : 1;
            }
        }
    }

    // Decision will not deal with timescales, as the menus that call it should be paused or not already
    public  void    CallbackWrapper ( int a ) {
        inDecision = false;
        optionMenu.Incoming ( false );
        if ( reqCallback != null ) {
            reqCallback ( a );
        }
    }
    
    public void RequestDecision ( Action<int> _callback, string OA, string OB, string title = "OPTIONS" ) {
        // Im mad that this is duplicated code ... >:(
        inDecision = true;
        reqCallback = _callback;
        optionMenu.Incoming ( true );
        optionMenu.Initiate ( CallbackWrapper, OA, OB, title );
    }

    public void CancelDecision () {
        if ( inDecision ) {
            CallbackWrapper ( 0 );
        }
    }
}
