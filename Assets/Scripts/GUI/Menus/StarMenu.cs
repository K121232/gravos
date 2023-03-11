using UnityEngine;
using TMPro;

public class StarMenu : MenuCore {
    public  Rigidbody2D     objA, objB;

    public  TMP_Text        vLabel1;
    public  RectTransform   vInd1;

    public  TMP_Text        vLabel2;
    public  RectTransform   vInd2;

    public  TMP_Text        dLabel;
    public  RectTransform   dInd;

    private void LateUpdate () {
        if ( status ) {
            vLabel1.text = objA.velocity.magnitude.ToString ( "0.00" );
            vInd1.rotation = Quaternion.FromToRotation ( Vector2.up, objA.velocity );
 
            vLabel2.text = objB.velocity.magnitude.ToString ( "0.00" );
            vInd2.rotation = Quaternion.FromToRotation ( Vector2.up, objB.velocity );

            dInd.rotation = Quaternion.FromToRotation ( Vector3.up, objB.position - objA.position );
            dLabel.text = ( objB.position - objA.position ).magnitude.ToString ( "0.00" );
        }
    }

    public void Update () {
        if ( Input.GetKeyDown ( KeyCode.X ) ) {
            Backflow ( !status );
        }
    }
}
