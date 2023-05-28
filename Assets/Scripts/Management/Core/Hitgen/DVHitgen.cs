using UnityEngine;
using System.Collections.Generic;

public class DVHitgen : Hitgen {
    // Delta V Hitgen
    private Rigidbody2D         rgb;
    
    private Vector3             pastPosition;
    private Vector3             pastV;

    public  float               velocityScalingSTR;

    public  float               invTime;
    private List<GameObject>    safeguard;
    private List<float>         timings;

    public virtual void Start () {
        safeguard = new List<GameObject> (0);
        timings = new List<float> ( 0 );
        if ( !TryGetComponent ( out rgb ) ) {
            rgb = GetComponentInParent<Rigidbody2D> ();
        }
        pastPosition = transform.position;
    }

    public override void Update () {
        base.Update ();
        while ( timings.Count > 0 && timings [ 0 ] <= Time.time ) {
            timings.RemoveAt ( 0 );
            safeguard.RemoveAt ( 0 );
        }
    }

    private void LateUpdate () {
        if ( rgb == null ) {
            pastV = ( transform.position - pastPosition );
            pastPosition = transform.position;
        }
    }

    public override int Bump ( GameObject who = null, Vector2? _deltaV = null ) {
        if ( who != null ) {
            if ( safeguard.Contains ( who ) ) {
                return 0;
            } else {
                safeguard   .Add ( who );
                timings     .Add ( Time.time + invTime );
            }
        }
        Rigidbody2D deltaRGBIN;
        Vector2 deltaV = Vector2.zero;
        if ( _deltaV != null ) {
            deltaV = _deltaV.Value;
        } else {
            if ( who.TryGetComponent ( out deltaRGBIN ) ) {
                if ( deltaV.magnitude < deltaRGBIN.velocity.magnitude ) {
                    deltaV = -deltaRGBIN.velocity;
                }
            }
        }
        if ( rgb && !rgb.isKinematic ) {
            deltaV += rgb.velocity;
        } else {
            //Debug.Log ( pastV );
            deltaV += (Vector2)( pastV ) / Time.deltaTime;
        }

        //Debug.Log ( Mathf.FloorToInt ( deltaV.magnitude * velocityScalingSTR ) );
        return base.Bump () + Mathf.FloorToInt ( deltaV.magnitude * velocityScalingSTR );
    }

}
