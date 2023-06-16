using System.Collections.Generic;
using UnityEngine;

public class Zetha : MonoBehaviour {
    [System.Serializable]
    public  struct HitboxWeight {
        public  ZethaMinion alpha;
        public  float       beta;
    }
    
    public  List<HitboxWeight>    hitboxes;
    public  float           baseHealth;
    public  float           currentHealth;

    public  PayloadStart     onDeath;

    public virtual void Start() {
        currentHealth = baseHealth;
        for ( int i = 0; i < hitboxes.Count; i++ ) {
            hitboxes [ i ].alpha.Subscribe ( this, i );
        }
    }

    public bool CheckID ( int id ) {
        return id >= 0 && id < hitboxes.Count;
    }

    public virtual void DeltaIncoming ( int id, float delta ) {
        CoreWHB ( id, delta );
        CoreODR ();
    }

    public void CoreWHB ( int id, float delta ) {         // Weighted Hitbox
        currentHealth -= hitboxes [ id ].beta * delta;
    }

    public void CoreODR () {                            // On death Recovery
        if ( currentHealth <= 0 ) {
            currentHealth = baseHealth;
            gameObject.SetActive ( false );
            onDeath.Deploy ( new PayloadObject ( Vector2.zero, transform.up, null ) );
        }
    }

    public  float   GetProcentHP() {
        if ( baseHealth == 0 ) return 0;
        return currentHealth / baseHealth;
    }

}
