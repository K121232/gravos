using UnityEngine;

public class PayloadStartAD : PayloadStart {
    public  float       lifespan;
    private  float      deltaT;

    public override void Deploy ( PayloadObject _instructions ) {
        base.Deploy ( _instructions );
        deltaT = lifespan;
        if ( instructions.expectedLifetime != -2 ) {
            deltaT = instructions.expectedLifetime;
        }
        if ( GetComponent<TrailRenderer>() != null ) {
            deltaT += GetComponent<TrailRenderer> ().time;
        }
    }

    public virtual void Update () {
        if ( deltaT > 0 ) {
            deltaT -= Time.deltaTime;
            if ( deltaT < 0 ) {
                gameObject.SetActive ( false );
            }
        }
    }
}
