using UnityEngine;

public class MenuManager : MonoBehaviour {
    [ System.Serializable]
    public struct MenuEntry {
        public  MenuCore    menuHandle;
        public  bool        status;
        public  int         prio;
    }

    private int             currentPrio;
    public bool menuLock { get; private set; }
    public  MenuEntry[]     menus;

    private void Start () {
        for ( int i = 0; i < menus.Length; i++  ) {
            menus [ i ].menuHandle.Handshake ( this, i );
            menus [ i ].status = false;
        }
        currentPrio = -1;
    }

    private bool delta;

    public  void    MenuRequest( int id, bool target ) {
        if ( target && currentPrio > menus [ id ].prio ) {
            return;
        }
        currentPrio = -1;
        for ( int i = 0; i < menus.Length; i++ ) {
            delta = i == id ? target : ( !target ? menus[ i ].status : false );
            if ( delta != menus [ i ].status ) {
                menus [ i ].menuHandle.Incoming ( menus [ i ].status );
                menus [ i ].status = delta;
            }
            if ( menus[i].status ) {
                currentPrio = menus [ i ].prio;
            }
        }
    }

}
