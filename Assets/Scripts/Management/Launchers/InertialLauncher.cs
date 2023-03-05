using UnityEngine;

public class InertialLauncher : Launcher {
    public  float       mxV;
    public  float       mnV;
    
    public override void Fire() {
        GameObject delta = autoLoader.Request();
        if ( delta.GetComponent<InertialImpactor> () != null ) {
            delta.GetComponent<InertialImpactor> ().velocity = Random.Range ( mnV, mxV );
        }
        float targetDistance = Random.Range ( minRange, maxRange );
        delta.transform.SetPositionAndRotation( transform.position + (Vector3)Random.insideUnitCircle.normalized * targetDistance, Quaternion.Euler( 0, 0, 360 * Random.value ) );
        delta.SetActive( true );
    }
}