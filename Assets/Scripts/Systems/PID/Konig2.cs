using UnityEngine;

public class Konig2 : MonoBehaviour {
    public float Kp;
    public float Ki;
    public float Kd;

    private Vector2 integral;
    private Vector2 previousError;

    // MAJOR WARNINGS : MUST BE UPDATED IN LOCKSTEP WITH THE TARGET, OR IT LOSES SYNC

    public Vector2 NextFrame ( Vector2 target, Vector2 current, float deltaTime ) {
        if ( deltaTime == 0 ) { return Vector2.zero; }
        Vector2 error = target - current;
        integral += error * deltaTime;
        Vector2 derivative = (error - previousError) / deltaTime;

        Vector2 control = (Kp * error) + (Ki * integral) + (Kd * derivative);

        previousError = error;
        return control;
    }

    public void Reset () {
        integral = Vector2.zero;
        previousError = Vector2.zero;
    }
}
