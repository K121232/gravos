using UnityEngine;

public class LineConnector : MonoBehaviour {
    private  LineRenderer    lineRenderer;

    public  Transform       objectA;
    public  Transform       objectB;

    public  float           attachLength;

    private void Start () {
        lineRenderer = GetComponent<LineRenderer> ();
    }

    private void OnEnable () {
        if ( lineRenderer != null )
            lineRenderer.enabled = enabled;
    }

    private void OnDisable () {
        if ( lineRenderer != null )
            lineRenderer.enabled = enabled;
    }

    public void LoadAsDelta ( Transform alpha ) {
        objectB = alpha;
        enabled = true;
    }

    private void LateUpdate () {
        if ( objectA == null || objectB == null ) { enabled = false; return; }

        Vector2 delta   = objectA.position - objectB.position;
        float deltaR;
        if ( attachLength != 0 ) {
            deltaR  = ( delta.magnitude / attachLength );
        } else {
            deltaR = 1;
        }
        Color deltaC    = Color.Lerp ( Color.black, Color.red, deltaR );

        lineRenderer.startColor = deltaC;
        lineRenderer.endColor   = lineRenderer.startColor;
        lineRenderer.SetPosition ( 0, objectA.position );
        lineRenderer.SetPosition ( 1, objectB.position );
    }
}
