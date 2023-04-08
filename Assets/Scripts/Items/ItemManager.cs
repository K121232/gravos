using UnityEngine;

public class ItemManager : MonoBehaviour {
    public  ItemPort[]      ports;
    public  PoolSpooler     capsulePool;

    public void Jettison ( int target ) {
        // DO some thing here, maybe use a turret to launch them, as they would inherit velocity
        if ( target >= 0 && target < ports.Length && ports [ target ].item != null ) {
            //ports [ target ].item.Attach ();
        }
    }

    public void JettisonAll () {

    }
}
