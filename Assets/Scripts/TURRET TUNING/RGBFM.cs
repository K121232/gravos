using UnityEngine;

public class RGBFM : MonoBehaviour {
    private Rigidbody2D rgb;

    public  float       loopspan;
    private float       deltaT;

    public  float       magnitude;

    public enum SELECTION { STATIC, LINEAR, LINEAR2, ROUND, ROUND2 }
    public  SELECTION trajectory;

    void Start () {
        rgb = GetComponent<Rigidbody2D>();  
    }

    public virtual Vector2  FX ( float v ) {
        v *= 2 * Mathf.PI;
        switch ( trajectory ) {
            case SELECTION.STATIC:
                return Vector2.zero;
            case SELECTION.LINEAR:
                return Vector2.right * Mathf.Sin ( v ) * magnitude;
            case SELECTION.LINEAR2:
                return ( new Vector2 ( 1, 1 ) ).normalized * Mathf.Sin ( v ) * magnitude;
            case SELECTION.ROUND:
                return new Vector2 ( Mathf.Cos ( v ), Mathf.Sin ( v ) ) * magnitude;
            case SELECTION.ROUND2:
                return new Vector2 ( Mathf.Sin ( v ), Mathf.Cos ( v ) ) * magnitude;
        }
        return Vector2.zero;
    }

    void FixedUpdate () {
        deltaT += Time.fixedDeltaTime;
        deltaT -= Mathf.FloorToInt ( deltaT / loopspan ) * deltaT;
        rgb.velocity = FX ( deltaT / loopspan );
    }
}
