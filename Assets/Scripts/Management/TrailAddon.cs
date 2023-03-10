using UnityEngine;

public class TrailAddon : Autodeleter {
    public  Transform       target;
    private TrailRenderer   tr;

    public void Bind ( Transform alpha ) {
        target = alpha;
        tr = GetComponent<TrailRenderer> ();
        var delta = alpha.GetComponent<Autodeleter>();
        if ( delta != null && delta.lifespan != -1 ) {
            lifespan = delta.lifespan + tr.time;
        }
        if ( tr != null ) { tr.Clear (); }
        transform.position = alpha.position;
    }

    public override void OnEnable () {
        tr = GetComponent<TrailRenderer>();
        base.OnEnable ();
    }
    void LateUpdate () {
        transform.position = target.position;
    }
}
