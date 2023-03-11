using UnityEngine;

public class DirectionIndicator : MonoBehaviour {
    private     EQGrapple       eqMain;
    private     Rigidbody2D     headRGB;

    public  Transform       indicator;
    public  float           STRV;

    private bool            deltaC; // Delta check

    private void Start () {
        eqMain = GetComponent<EQGrapple> ();
        deltaC = true;
    }

    void LateUpdate () {
        bool delta = eqMain.headLaunched && !eqMain.head.detached;
        if ( delta != deltaC ) {
            if ( delta ) {
                headRGB = eqMain.head.GetAnchorRGB ();
            } else {
                headRGB = null;
            }
            indicator.gameObject.SetActive ( delta );
        }
        if ( delta ) {
            indicator.rotation = Quaternion.FromToRotation ( Vector2.up, headRGB.velocity );
            float deltaMY = 0.6f * Mathf.Round ( headRGB.velocity.magnitude * STRV );
            indicator.GetChild ( 0 ).GetComponent<SpriteRenderer> ().size = new Vector2 ( 1, deltaMY );
        }
        deltaC = delta;
    }
}
