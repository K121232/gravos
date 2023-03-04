using System.Collections.Generic;
using UnityEngine;

public class Zetha : MonoBehaviour {
    [System.Serializable]
    public  struct HitboxWeight {
        public  Hitbox      alpha;
        public  float       beta;
    }
    
    public  List<HitboxWeight>    hitboxes;
    public  float           baseHealth;
    public  float           currentHealth;   

    void Start() {
        currentHealth = baseHealth;
    }

    public  int     Subscribe ( Hitbox alpha ) {
        for ( int i = 0; i < hitboxes.Count; i++ ) {
            if ( hitboxes [ i ].alpha == alpha ) {
                return i;
            }
        }
        return -1;
    }

    public bool CheckID ( int id ) {
        return id >= 0 && id < hitboxes.Count;
    }

    public virtual void DeltaIncoming ( int id, int delta ) {
    }

    public void CoreWHB ( int id, int delta ) {         // Weighted Hitbox
        currentHealth -= hitboxes [ id ].beta * delta;
    }

    public void CoreODR () {                            // On death Recovery
        if ( currentHealth <= 0 ) {
            currentHealth = baseHealth;
            gameObject.SetActive ( false );
        }
    }

}
