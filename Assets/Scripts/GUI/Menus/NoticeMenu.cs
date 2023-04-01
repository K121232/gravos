using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NoticeMenu : MenuCore {
    public  TMP_Text    label;
    public  void SendNotice ( string a ) {
        label.text = a;
        Backflow ( true );
    }

    public override void Incoming ( bool a ) {
        base.Incoming ( a );
    }

    public void ButtonReset () {
        SceneManager.LoadScene ( 0 );
    }

    public override void Update () {
        if ( Input.GetKeyDown ( KeyCode.N ) ) {
            Backflow ( !status );
        }
        base.Update ();
    }
}
