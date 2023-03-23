using UnityEngine;
using System.Collections.Generic;

public class BladeHitgen : Hitgen {
    private Rigidbody2D         rgb;
    public  float               velocityScalingSTR;

    public  float               invTime;
    private List<GameObject>    safeguard;
    private List<float>         timings;

    private void Start () {
        safeguard = new List<GameObject> (0);
        timings = new List<float> ( 0 );
        if ( !TryGetComponent ( out rgb ) ) {
            rgb = GetComponentInParent<Rigidbody2D> ();
        }
    }

    private void Update () {
        while ( timings.Count > 0 && timings [ 0 ] <= Time.time ) {
            timings.RemoveAt ( 0 );
            safeguard.RemoveAt ( 0 );
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
            if ( rgb ) {
                deltaV += rgb.velocity;
            }
        }

        Debug.Log ( Mathf.FloorToInt ( deltaV.magnitude * velocityScalingSTR ) );
        return base.Bump () + Mathf.FloorToInt ( deltaV.magnitude * velocityScalingSTR );
    }

}
