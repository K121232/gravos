using UnityEngine;

public class InertialLauncher : Launcher {
    public  float       mxV;
    public  float       mnV;
    
    public override void Fire() {
        GameObject delta = autoLoader.Request();
        delta.GetComponent<InertialImpactor>().velocity = Random.Range( mnV, mxV );
        delta.transform.SetPositionAndRotation( transform.position + ( ( Vector3 )Random.insideUnitCircle * maxRange ), Quaternion.Euler( 0, 0, 360 * Random.value ) );
        delta.SetActive( true );
    }
}