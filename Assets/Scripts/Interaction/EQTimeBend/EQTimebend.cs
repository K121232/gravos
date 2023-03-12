using UnityEngine;

public class EQTimebend : MonoBehaviour {
    public  float       bendSTR;
    public  PowerCell   cell;

    public void Update () {
        if ( Input.GetKey ( KeyCode.Space ) && cell.Available() ) {
            cell.VariDrain ( Time.unscaledDeltaTime );
            Time.timeScale = bendSTR;
        } else {
            Time.timeScale = 1;
        }
    }

}
