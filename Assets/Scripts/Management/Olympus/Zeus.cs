using UnityEngine;

public class Zeus : MonoBehaviour {
    public  ItemPort        port;
    public  Thunder[]       turrets;

    public  bool            launchIsDetectable = true;
    public  bool            firingLock;

    public  Transform       trackedTarget;

    protected virtual void Start () {
        for ( int i = 0; i < turrets.Length; i++ ) {
            turrets [ i ].MainInit ( port );
        }
    }

    // Turret modify operation
    public virtual void InternalTurretMOP ( Transform target, Thunder alpha ) {
        alpha.SetTarget ( target );
    }

    // Set firing lock and unload target if lock is engaged
    public virtual void SetFiringLock ( bool alpha ) {
        firingLock = alpha;
        if ( firingLock ) {
            ModifyTarget ( null );
        }
    }

    public virtual bool IsTracking () {
        return trackedTarget != null;
    }

    public virtual void ModifyTarget ( Transform _target ) {
        if ( firingLock ) { _target = null; }
        
        trackedTarget = _target;

        for ( int i = 0; i < turrets.Length; i++ ) {
            if ( turrets [ i ] != null ) {
                InternalTurretMOP ( _target, turrets [ i ] );
            }
        }
    }
}
