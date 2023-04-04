using UnityEngine;
using System;
using TMPro;

public class Multihelper : MonoBehaviour {
    public  int             callID = -1;

    public  Transform[]     anchors;
    public  TMP_Text[]      labels;
    private Action<int>[]   callbacks;

    public void Init ( int _callID ) {
        callID = _callID;
        callbacks = new Action<int> [ 4 ];
    }

    public void SetCallback ( int target, Action<int> content ) {
        if ( target >= 0 && target < callbacks.Length ) {
            callbacks [ target ] = content;
        }
    }

    public void SetLabel ( int target, string content ) {
        if ( target >= 0 && target < labels.Length ) {
            labels [ target ].text = content;
        }
    }

    public void CallbackEntryPoint ( int selector ) {
        Debug.Log ( "CALLBACK ENTRY POINT TRIGGERED BY " + name );
        Action<int> deltaC = null;
        if ( selector >= 0 && selector < callbacks.Length ) {
            deltaC = callbacks [ selector ];
        }
        if ( deltaC != null && callID != -1 ) {
            Debug.Log ( "CALLBACK HAS BEEN CALLED" );
            deltaC ( callID );
        }
    }
}
