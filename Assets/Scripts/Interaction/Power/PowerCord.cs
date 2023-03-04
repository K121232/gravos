using UnityEngine;

public class PowerCord : MonoBehaviour {
    public float load { get { return ca.flow - cb.flow; } }
        
    [System.Serializable]
    public struct Cord {
        public  PowerNode   node;
        public  float       flow;
    }

    public  Cord    ca, cb;

    public float Interogate ( PowerNode a ) {
        if ( ca.node == a ) {
            return cb.flow;
        }
        return ca.flow;
    }

    public void Propagate ( PowerNode a, float amount ) {
        if ( ca.node == a ) {
            ca.flow = amount;
        } else {
            cb.flow = amount;
        }
    }

    public void Equalize ( PowerNode a ) {
        if ( ca.node == a ) {
            ca.flow = -cb.flow;
        }
        cb.flow = -ca.flow;
    }

}