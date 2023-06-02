using UnityEngine;

public class TFC : MonoBehaviour {
    protected   Thunder     controller;

    public void SetController ( Thunder thunder ) {
        controller = thunder;
    }

    public virtual float GetProgress () {
        return 1;
    }
}
