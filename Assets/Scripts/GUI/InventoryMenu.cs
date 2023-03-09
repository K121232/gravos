using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour {
    public  GameObject      mainPanel;
    private Animator        panelAnimation;

    private void Start () {
        panelAnimation = mainPanel.GetComponent<Animator> ();
    }

    void Update () {
        if ( Input.GetKeyDown ( KeyCode.P ) ) {
            panelAnimation.SetBool ( "Dispatch", !panelAnimation.GetBool ( "Dispatch" ) );
        }
    }
}
