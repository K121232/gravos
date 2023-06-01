using UnityEngine;

public class OmicronTM : TM {
    public  Konig2  targetingCaptain;

    public  Vector2 targetLocation;
    public  Vector2 targetVelocity;

    public  float   thrSTR;

    public override void Update () {
        thrustBypass = true;

        targetLink = targetingCaptain.NextFrame ( targetLocation, transform.position, Time.deltaTime );
        thurstLink = Mathf.Max ( Vector2.Dot ( transform.up, targetLink.normalized ), 0 ) * targetLink.magnitude * thrSTR;

        base.Update ();
    }
}
