using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class OptionMenu : MenuCore {
    public  TMP_Text    labelA, labelB, labelT;
    private Action<int>      callback;
    public  int         optionSelected;

    public  void    Initiate ( Action<int> _callback, string OA, string OB, string title = "OPTIONS" ) {
        // Should check if it is already active, so no overwrites takes place
        callback = _callback;
        labelA.text = OA;
        labelB.text = OB;
        labelT.text = title;
        labelA.transform.parent.GetComponent<Button> ().Select ();
    }

    public  void    OptionSelected ( int a ) {
        optionSelected = a;
        if ( callback != null ) {
            callback ( optionSelected );
        }
    }

    public override void Update () {
        if ( Input.GetKey ( KeyCode.Escape ) ) {
            OptionSelected ( 0 );
        }
    }
}
