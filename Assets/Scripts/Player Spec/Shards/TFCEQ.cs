using UnityEngine;

public class TFCEQ : TFC {
    public enum InteractionSlot { MAIN, SEC };
    public      InteractionSlot control;
    private     string  axisString;

    private void Start () {
        UpdateIS ( control );
    }

    public void UpdateIS ( InteractionSlot a ) {
        if ( a == InteractionSlot.MAIN ) {
            axisString = "Fire2";
        }
        if ( a == InteractionSlot.SEC ) {
            axisString = "Fire3";
        }
    }

    protected void Update () {
        if ( controller == null ) return;
        controller.ForceFire (
            Input.GetAxis ( axisString ) > 0
        );
    }
}
