using UnityEngine;

public class EffectCore : MonoBehaviour {
    public  float   time;
    private float   deltaTime;

    public  virtual void    Apply ( Transform root ) {
        
    }

    public virtual  void OnEnable () {

    }

    public virtual void OnDisable () {

    }

    void Update () {
        if ( deltaTime > 0 ) {
            deltaTime -= Time.deltaTime;
            if ( deltaTime < 0 ) {
                gameObject.SetActive ( false );
            }
        }
    }
}
