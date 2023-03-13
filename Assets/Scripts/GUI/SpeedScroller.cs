using UnityEngine;

public class SpeedScroller : MonoBehaviour {
    public  Rigidbody2D     rgb;
    public  float           strVEL;
    public  SpriteRenderer  targetSprite;
    private Material        mat;

    private float           deltaTime;

    private void Start () {
        mat = targetSprite.material;
    }

    void LateUpdate() {
        deltaTime += rgb.velocity.magnitude * strVEL * Time.deltaTime;
        mat.SetFloat( "_SANG", -Vector2.SignedAngle( rgb.velocity, Vector2.up ) );
        mat.SetFloat( "_SVEL", deltaTime );
        mat.SetVector( "_SRCENT", rgb.position );
    }
}
