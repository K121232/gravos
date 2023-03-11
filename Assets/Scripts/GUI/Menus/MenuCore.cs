using UnityEngine;

public class MenuCore : MonoBehaviour {
    protected MenuManager   manager;
    protected int           selfID = -1;
    protected Animator      animator;

    public  virtual void    Handshake ( MenuManager _manager, int _selfID ) {
        manager = _manager;
        selfID = _selfID;
    }

    public virtual  void    Incoming ( bool a ) {
        animator.SetBool ( "Dispatch", a );
    }

    public virtual void Start () {
        animator = GetComponent<Animator> ();
    }

    public  virtual void Backflow ( bool a ) {
        if ( manager != null && selfID != -1 ) {
            manager.MenuRequest ( selfID, a );
        }        
    }
}
