using UnityEngine;
using UnityEngine.UI;

public class BriefingMenu : MenuCore {
    public  GameObject[]    pages;
    private int             iter;

    public  Button          buttonP;
    public  Button          buttonN;

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

    public void Update () {
        if ( Input.GetKeyDown ( KeyCode.B ) ) {
            Backflow ( !status );
            FlipPage ( -pages.Length );
        }
    }
}
