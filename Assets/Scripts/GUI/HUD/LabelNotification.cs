using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LabelNotification : MonoBehaviour {
    public  TMP_Text            notificationLabel;
    public  TMP_Text            notificationLabelSec;
    public  List<DataLinkNTF>  ntfQ;

    void Start () {
        ntfQ = new List<DataLinkNTF> ( 5 );
    }

    string Filter ( string a ) {
        return "[ " + a + " ]"; 
    }

    void LateUpdate () {
        for ( int i = 0; i < ntfQ.Count; i++ ) {
            if ( ntfQ [ i ].timeout != -1 ) {
                ntfQ [ i ].timeout -= Time.deltaTime;
                if ( ntfQ [ i ].timeout <= 0 ) {
                    ntfQ.RemoveAt ( i );
                    i--;
                    continue;
                }
            }
        }
        if ( ntfQ.Count != 0 ) {
            notificationLabel.text = Filter ( ntfQ [ 0 ].content );
        } else {
            notificationLabel.text = "";
        }

        if ( ntfQ.Count >= 2 ) {
            notificationLabelSec.text = Filter ( ntfQ [ 1 ].content );
        } else {
            notificationLabelSec.text = "";
        }
    }

    public  void    AddMessage ( DataLinkNTF a ) {
        if ( ntfQ == null ) { Start (); }
        ntfQ.Add ( a );
        ntfQ.Sort ();
    }

    public  void    RemoveMessage ( string _content ) {
        for ( int i = 0; i < ntfQ.Count; i++ ) {
            if ( ntfQ [ i ].content == _content ) {
                ntfQ.RemoveAt ( i );
                return;
            }
        }
    }
}
