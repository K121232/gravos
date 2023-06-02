using UnityEngine;

public class UnifiedOrdnance : MonoBehaviour, UnifiedOrdnanceFireInterface {
    // Basic class for the FCM to be on the lookout for, the other scripts will implement the actual interfaces
    protected   Thunder     controller;

    public virtual void MainInit ( Thunder _controller ) {
        controller = _controller;
    }

    public virtual void OnStartFire () { }

    public virtual void OnStopFire () { }

    public virtual void Fire () { }
}
