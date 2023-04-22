using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class DataLinkNTF : IComparable {
    public  string      content = " !!! ";
    public  float       timeout = 5;            // If timeout = -1 then it stays on
    public  int         prio = 0;
    public DataLinkNTF ( string _content = " !!! ", float _timeout = -1, int _prio = 0 ) {
        content = _content;
        timeout = _timeout;
        prio = _prio;
    }

    public int CompareTo ( object obj ) {
        DataLinkNTF other = obj as DataLinkNTF;
        if ( prio == other.prio ) return 0;
        if ( prio == -1 ) return -1;
        if ( other.prio == -1 ) return 1;
        if ( prio > other.prio ) return -1;
        return 1;
    }
}

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
