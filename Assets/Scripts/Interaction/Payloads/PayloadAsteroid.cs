using UnityEngine;

public class PayloadAsteroid : MonoBehaviour {
    public  float   minAV;
    public  float   maxAV;

    public  float   minV;
    public  float   maxV;

    private void OnEnable () {
        Rigidbody2D delta = GetComponent<Rigidbody2D>();
        delta.velocity = Random.insideUnitCircle.normalized * Random.Range ( minV, maxV );
        delta.angularVelocity = Random.Range ( minAV, maxAV ) * Mathf.Sign ( Random.Range ( -10, 10 ) );
    }
}