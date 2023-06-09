using UnityEngine;

public class FlightPlanner : MonoBehaviour {
    public  Transform       handlesRoot;

    public LRCTM            targetTM;

    public  float           scale = 1;

    public  Vector3         offset;

    public void UpdateCoordinates () {
        for ( int i = 0; i < handlesRoot.childCount; i++ ) {
            if ( i % 2 == 0 ) {
                targetTM.positionPoints [ i / 2 ] = ( offset + handlesRoot.GetChild ( i ).position ) * scale;
            } else {
                targetTM.controlPoints [ i / 2 ] = ( offset + handlesRoot.GetChild ( i ).position ) * scale;
            }
        }
    }

    public void Start () {
        targetTM.controlPoints  = new Vector2 [ handlesRoot.childCount / 2 ];
        targetTM.positionPoints = new Vector2 [ handlesRoot.childCount / 2 ];
        UpdateCoordinates ();
    }

    private void Update () {
        UpdateCoordinates ();
    }
}
