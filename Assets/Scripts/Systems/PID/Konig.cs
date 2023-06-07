using UnityEngine;

public class Konig : MonoBehaviour {
    public float Kp;
    public float Ki;
    public float Kd;

    private float integral;
    private float previousError;

    // MAJOR WARNING : MUST BE UPDATED IN LOCKSTEP WITH THE TARGET, OR IT LOSES SYNC

    public float NextFrame ( float target, float current, float deltaTime ) {
        if ( deltaTime == 0 ) { return 0; }
        float error = target - current;
        integral += error * deltaTime;
        float derivative = (error - previousError) / deltaTime;

        float control = (Kp * error) + (Ki * integral) + (Kd * derivative);

        previousError = error;
        return control;
    }

    public void Reset () {
        integral = 0f;
        previousError = 0f;
    }
}
