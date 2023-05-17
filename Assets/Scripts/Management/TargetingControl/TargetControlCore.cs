using UnityEngine;

public class TargetControlCore : MonoBehaviour {
    public  TargetingRig[]  turrets;
    
    public  bool            launchIsDetectable = true;
    public  bool            firingLock;

    protected   Transform       targetDelta;

    // Turret modify operation
    public  virtual void    InternalTurretMOP ( TargetingRig alpha ) {
        alpha.LoadTarget ( targetDelta );
    }

    // Set firing lock and unload target if lock is engaged
    public  virtual void    SetFiringLock ( bool alpha ) {
        firingLock = alpha;
        if ( firingLock ) {
            ModifyTarget ( null );
        }
    }

    public virtual bool IsTracking () {
        return targetDelta != null;
    }

    public virtual void ModifyTarget ( Transform _target ) {
        if ( firingLock ) { _target = null; }
        targetDelta = _target;
        for ( int i = 0; i < turrets.Length; i++ ) {
            if ( turrets [ i ] != null ) {
                InternalTurretMOP ( turrets [ i ] );
            }
        }
    }
}
