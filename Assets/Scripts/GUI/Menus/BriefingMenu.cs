using UnityEngine;
using UnityEngine.UI;

public class BriefingMenu : MenuCore {
    public  Transform       pagesRoot;
    private GameObject[]    pages;
    private int             iter;

    public  Button          buttonP;
    public  Button          buttonN;

    public override void Start () {
        pages = new GameObject [ pagesRoot.childCount ];
        for ( int i = 0; i < pagesRoot.childCount; i++ ) {
            pages [ i ] = pagesRoot.GetChild ( i ).gameObject;
            
        }
        base.Start ();
    }

    public override void Incoming ( bool a ) {
        FlipPage ( -pages.Length );
        base.Incoming ( a ); 
    }

    public  void    FlipPage ( int alpha ) {
        pages [ iter ].SetActive ( false );
        iter += alpha;
        if ( iter >= pages.Length ) {
            iter = pages.Length - 1;
        }
        if ( iter < 0 ) {
            iter = 0;
        }
        buttonP.interactable = iter != 0;
        buttonN.interactable = iter != pages.Length - 1;

        pages [ iter ].SetActive ( true );
    }

    public override void Update () {
        if ( Input.GetKeyDown ( KeyCode.B ) ) {
            Backflow ( !status );
            FlipPage ( -pages.Length );
        }
        base.Update ();
    }
}
