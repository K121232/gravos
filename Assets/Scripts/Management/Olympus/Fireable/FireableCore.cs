using UnityEngine;

public class FireableCore : MonoBehaviour, INTFCM {
    public  Thunder     controller;

    public  PoolSpooler payloadLoader;
    public  PoolSpooler trailLoader;

    public GameObject Fire () { return null; }

    public virtual void MainInit ( Thunder _controller ) {
        controller = _controller;
    }
}
