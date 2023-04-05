using UnityEngine;

public class ItemPorts : MonoBehaviour {
    public  PoolSpooler     shellPool;
    public  GameObject      mainHull;
    public  Transform       storeRoot;

    public  float           spawnRange;
    public  float           jettisonSpeed;

    public void Jettison ( int id, int quantity = -1 ) {
        Debug.Log ( storeRoot.GetChild ( id ).name + " JETTISONED " );
        GameObject  delta = shellPool.Request();
        
        ItemHandle deltaIH = storeRoot.GetChild ( id ).GetComponent<ItemHandle> ();
        deltaIH.Attach ( delta );

        if ( quantity == -1 || quantity >= deltaIH.itemQuantity ) {
            deltaIH.enabled = false;
            Destroy ( storeRoot.GetChild ( id ).gameObject );
        } else {
            deltaIH.itemQuantity -= quantity;
        }

        delta.transform.position = mainHull.transform.position + (Vector3) Random.insideUnitCircle.normalized * spawnRange;
        delta.SetActive ( true );
        delta.GetComponent<Rigidbody2D> ().velocity = ( delta.transform.position - mainHull.transform.position ).normalized * jettisonSpeed;
    }

    public  void    JettisonAll () {
        ItemHandle delta;
        for ( int i = 0; i < storeRoot.childCount; i++ ) {
            delta = storeRoot.GetChild ( i ).GetComponent<ItemHandle> ();
            if ( delta != null ) {
                Jettison ( i );
                i--;
            }
        }
    }

    public  void Store ( GameObject cargo ) {
        cargo.transform.GetChild( 0 ).GetComponent<ItemHandle> ().Attach ( mainHull );
        // return the cargo to the pool
        cargo.SetActive ( false );
    }
}
