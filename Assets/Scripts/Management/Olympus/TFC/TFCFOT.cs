using UnityEngine;

// Fire on target
public class TFCFOT : TFC {
    protected void Update () {
        if ( controller == null ) return;
        controller.ForceFire ( controller.target != null );
    }

    public override float GetProgress () {
        if ( controller.target == null ) return 0;
        return controller.GetFCMP();
    }
}
