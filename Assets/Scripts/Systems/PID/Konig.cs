using UnityEngine;

public class Konig : MonoBehaviour {
    public float Kp;
    public float Ki;
    public float Kd;

    private float   integral;
    
    private int     filterSize = 10, filterIndex = 0;
    private float[] integralFilter;

    private float   previousError;

    // MAJOR WARNING : MUST BE UPDATED IN LOCKSTEP WITH THE TARGET, OR IT LOSES SYNC

    private void Start () {
        integralFilter = new float [ filterSize ];
        Reset ();
    }

    public float NextFrame ( float target, float current, float deltaTime ) {
        if ( deltaTime == 0 ) { return 0; }
        float error = target - current;
        
        UpdateIntegral ( error * deltaTime );

        float derivative = (error - previousError) / deltaTime;

        float control = (Kp * error) + (Ki * integral) + (Kd * derivative);

        previousError = error;
        return control;
    }

    public void Reset () {
        integral = 0f;
        previousError = 0f;
        integralFilter = new float [ filterSize ];
        for ( int i = 0; i < filterSize; i++ ) {
            integralFilter[ i ] = 0;
        }
        filterIndex = 0;
    }

    public void UpdateIntegral ( float _alpha ) {
        if ( integralFilter == null || integralFilter.Length == 0 ) { Start (); }
        integral -= integralFilter [ filterIndex ];
        integralFilter [ filterIndex ] = _alpha / filterSize;
        integral += integralFilter [ filterIndex ];
        filterIndex = ( filterIndex + 1 ) % filterSize;
    }
}
