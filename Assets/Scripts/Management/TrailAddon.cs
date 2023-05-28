using UnityEngine;

public class TrailAddon : Autodisabler {
    public  Transform       target;
    private TrailRenderer   tr;

    public void Bind ( Transform alpha ) {
        target = alpha;
        tr = GetComponent<TrailRenderer> ();
        var delta = alpha.GetComponent<Autodisabler>();
        if ( delta != null && delta.lifespan != -1 ) {
            lifespan = delta.lifespan + tr.time;
        }
        if ( tr != null ) { tr.Clear (); }
        transform.position = alpha.position;
    }

    public override void OnEnable () {
        tr = GetComponent<TrailRenderer> ();
        base.OnEnable ();
    }

    private void OnDisable () {
        if ( tr != null ) {
            tr = GetComponent<TrailRenderer> ();
        }
        tr.Clear ();
    }

    void LateUpdate () {
        if ( target != null ) {
            transform.position = target.position;
        }
    }
}
