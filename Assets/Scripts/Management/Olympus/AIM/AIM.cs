using UnityEngine;

public class AIM : MonoBehaviour {
    protected   Thunder     controller;
    protected float       pastDeviation;

    public virtual void SetController ( Thunder _thunder ) {
        controller = _thunder;
    }

    public virtual void ResetPD () {
        pastDeviation = Mathf.Infinity;
    }

    public virtual float GetLastDeviation () { return pastDeviation; }
}
