using UnityEngine;

public class TargetControlCore : MonoBehaviour {
    public  TargetingRig[]  turrets;
    
    public  bool            launchIsDetectable = true;
    public  bool            firingLock;

    protected   Transform       targetDelta;

    public  virtual void    ModifyTurretOp ( TargetingRig alpha ) {
        alpha.LoadTarget ( targetDelta );
    }

    public  virtual void    ModifyLock ( bool alpha ) {
        firingLock = alpha;
        ModifyTarget ( null );
    }

    public  virtual void    ModifyTarget ( Transform _target ) {
        if ( firingLock ) { _target = null; }
        if ( targetDelta != _target ) {
            targetDelta = _target;
            for ( int i = 0; i < turrets.Length; i++ ) {
                if ( turrets [ i ] != null ) {
                    ModifyTurretOp ( turrets [ i ] );
                }
            }
        }
    }

    public virtual bool IsTracking () {
        return targetDelta != null;
    }
}
