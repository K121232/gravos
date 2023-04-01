using UnityEngine;

public class OmicronTM : TM {
    public  Theo    targetingCaptain;

    public  Vector2 targetLocation;
    public  Vector2 targetVelocity;

    public  float   thrSTR;

    public override void Update () {
        thrustBypass = true;
        targetLink = targetingCaptain.NextFrame ( Time.fixedDeltaTime, targetLocation - (Vector2) transform.position, targetLocation, rgb.velocity - targetVelocity, targetVelocity );

        thurstLink = Mathf.Max ( Vector2.Dot ( transform.up, targetLink.normalized ), 0 ) * targetLink.magnitude * thrSTR;
        base.Update ();
    }
}
