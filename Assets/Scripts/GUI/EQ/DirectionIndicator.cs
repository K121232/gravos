using UnityEngine;

public class DirectionIndicator : MonoBehaviour {
    private     EQGrapple       eqMain;
    private     Rigidbody2D     hookRgb;

    public  Transform       indicator;
    public  float           STRV;

    private bool            deltaC; // Delta check

    private void Start () {
        eqMain = GetComponent<EQGrapple> ();
        deltaC = true;
    }

    void LateUpdate () {
        bool delta = eqMain.hookLaunched && !eqMain.hookLink.detached;
        if ( delta != deltaC ) {
            if ( delta ) {
                hookRgb = eqMain.lineTeth.objectB;
            } else {
                hookRgb = null;
            }
            indicator.gameObject.SetActive ( delta );
        }
        float deltaMY = 0;
        if ( delta && hookRgb != null ) {
            indicator.rotation = Quaternion.FromToRotation ( Vector2.up, hookRgb.velocity );
            deltaMY = 0.6f * Mathf.Round ( hookRgb.velocity.magnitude * STRV );
        }
        indicator.GetChild ( 0 ).GetComponent<SpriteRenderer> ().size = new Vector2 ( 1, deltaMY );
        deltaC = delta;
    }
}
