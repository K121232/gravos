using UnityEngine;
using System.Collections.Generic;

public class BladeHitgen : Hitgen {
    public  float               invTime;
    private List<GameObject>    safeguard;
    private List<float>         timings;

    private void Start () {
        safeguard = new List<GameObject> (0);
        timings = new List<float> ( 0 );
    }

    private void Update () {
        while ( timings.Count > 0 && timings [ 0 ] <= Time.time ) {
            timings.RemoveAt ( 0 );
            safeguard.RemoveAt ( 0 );
        }
    }

    public override int Bump ( GameObject who = null ) {
        if ( who != null ) {
            if ( safeguard.Contains ( who ) ) {
                return 0;
            } else {
                safeguard   .Add ( who );
                timings     .Add ( Time.time + invTime );
            }
        }
        return base.Bump ();
    }

}
