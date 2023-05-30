using UnityEngine;

public class FireableCore : MonoBehaviour, INTFCM, ThunderMinion {
    protected   Thunder     controller;

    public      PoolSpooler payloadLoader;
    public      PoolSpooler trailLoader;

    public void SetController ( Thunder thunder ) {
        controller = thunder;
    }

    public virtual GameObject Fire () { return null; }

    public virtual void MainInit ( Thunder _controller ) {
        controller = _controller;
    }
}
