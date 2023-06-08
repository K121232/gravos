using UnityEngine;

public class AIMSNAP : AIM {
    private void Update () {
        if ( controller != null && controller.target != null ) {
            transform.rotation = Quaternion.FromToRotation ( Vector2.up, controller.target.position - transform.position );
        } else {
            transform.rotation = Quaternion.identity;
        }
    }

    public override float GetLastDeviation () {
        if ( controller == null || controller.target == null ) return Mathf.Infinity;
        return 0;
    }
}
