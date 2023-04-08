using UnityEngine;

public class EQTimebend : EQBase {
    [Header ( "EQ Time Bend" )]
    public  float       bendSTR;
    public  PowerCell   cell;

    private float       pastScale;
    private bool        pastStatus, delta;

    public override void MainInit ( ItemPort port ) {
        if ( port == null ) return;
        base.MainInit ( port );
        Debug.Log ( "SKREEEEEEEEE" );
        cell = port.batteryLink.GetChild ( 1 ).GetComponent<PowerCell> ();
    }

    public void Update () {
        delta = Input.GetAxis ("Fire3") > 0 && cell.Available ();
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
