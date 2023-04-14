using UnityEngine;

public class TMFuse : MonoBehaviour {
    private Rigidbody2D rgb;

    public  float   launchVelocity;
    public  float   launchDelay;
    private float   deltaT;

    private Vector2 launchVector;

    private TM      targetTM;
    
    public  void Bind ( Vector2 initV, Vector2 _launchVector ) {
        launchVector = initV + _launchVector * launchVelocity;
    }

    private void Start () {
        targetTM    = GetComponent<TM> ();
        rgb         = GetComponent<Rigidbody2D> ();
    }

    private void OnEnable () {
        if ( rgb == null ) Start();
        deltaT = launchDelay;
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
