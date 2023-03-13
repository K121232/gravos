using UnityEngine;

public class Shockwave : MonoBehaviour {
    public  float   maxScale;
    public  float   expansionTime;

    private float   deltaTime;

    private void OnEnable () {
        transform.localScale = Vector3.one;
        deltaTime = expansionTime;
        this.enabled = true;
        if ( expansionTime == 0 ) gameObject.SetActive ( false );
    }

    void Update () {
        deltaTime -= Time.deltaTime;
        transform.localScale = Vector3.one * Mathf.Lerp ( 1, maxScale, 1 - deltaTime / expansionTime );
        if ( deltaTime > expansionTime ) {
            this.enabled = false;
        }
    }
}
