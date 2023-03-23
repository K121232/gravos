using UnityEngine;

public class TMFuse : MonoBehaviour {
    private Rigidbody2D rgb;

    public  float   launchVelocity;
    public  float   launchDelay;
    private float   deltaT;

    private Vector2 launchVector;

    private TM      targetTM;
    
    public  void Bind ( Transform mainHull, Vector2 inheritedV ) {
        Vector2 delta = transform.position - mainHull.position;
        delta = Vector3.Project ( delta, mainHull.right );
        launchVector = inheritedV + delta.normalized * launchVelocity;
    }

    private void OnEnable () {
        if ( rgb == null ) rgb = GetComponent<Rigidbody2D> ();
        deltaT = launchDelay;
        targetTM = GetComponent<TM> ();
        targetTM.enabled = false;
        rgb.velocity = launchVector;
    }

    private void Update () {
        deltaT -= Time.deltaTime;
        if ( deltaT < 0 ) {
            deltaT = 0;
            targetTM.enabled = true;
        }
    }

}
