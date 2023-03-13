using UnityEngine;

public class EQTimebend : MonoBehaviour {
    public  float       bendSTR;
    public  PowerCell   cell;

    private float       pastScale;
    private bool        pastStatus, delta;

    public void Update () {
        delta = Input.GetKey ( KeyCode.Space ) && cell.Available ();
        if ( delta ) {
            cell.VariDrain ( Time.unscaledDeltaTime );
        }
        if ( delta != pastStatus ) {
            if ( !pastStatus ) pastScale = Time.timeScale;
            Time.timeScale = delta ? bendSTR : pastScale;
        }
        pastStatus = delta;
    }

}
