using UnityEngine;

public class ForceTether : MonoBehaviour {
    public  Rigidbody2D     objectA;
    public  Transform           attachPointA;
    public  Rigidbody2D     objectB;
    public  Transform           attachPointB;

    public  float       velocityDampeningStrength;
    public  float       linearSpringStrength;

    public  float       orbitalDampeningStrength;
    public  float       orbitalDampeningPower;

    private Vector2     forceAccumulator;
    private float       attachLength;

    public  float       distribution = 0.5f;

    public  bool        backflow = false;

    private void Start () {
        SanityCheck ();
    }

    public void BaseInit ( Rigidbody2D _objectA, Transform _attachPointA ) {
        objectA = _objectA;
        attachPointA = _attachPointA;
    }

    public  void    LoadBL ( Rigidbody2D _objectB, Transform _attachPointB, float _attachLength ) {     // Object B and attach length
        objectB = _objectB;
        attachLength = _attachLength;
        attachPointB = _attachPointB;
        SanityCheck ();
    }

    public void SanityCheck () {
        if ( ( objectA == null && objectB == null ) ||
             attachPointA == null || attachPointB == null ||
             attachLength == 0 ) { enabled = false; }
    }

    void FixedUpdate () {
        Vector2 delta   = attachPointB.position - attachPointA.position;
        if ( delta.magnitude > attachLength ) {
            Vector2 deltaDir    = delta.normalized;
            Vector2 deltaV      = Vector3.zero;
            
            if ( objectA != null ) deltaV += objectA.velocity;
            if ( objectB != null ) deltaV -= objectB.velocity;

            Vector2 deltaVT     = Vector3.Project( deltaV, Quaternion.Euler( 0, 0, 90 ) * deltaDir );
            float   deltaS      = Vector3.Dot ( deltaV, deltaDir );

            forceAccumulator = deltaDir * ( delta.magnitude - attachLength ) * linearSpringStrength;
            forceAccumulator -= deltaDir * velocityDampeningStrength * deltaS;
            forceAccumulator -= deltaVT.normalized * Mathf.Pow ( deltaVT.magnitude, orbitalDampeningPower ) * orbitalDampeningStrength;

            if ( Vector2.Dot ( forceAccumulator.normalized, deltaDir ) < 0 && !backflow ) {
                forceAccumulator -= (Vector2)Vector3.Project ( forceAccumulator, deltaDir );
            }

            if ( objectA != null ) {
                objectA.AddForceAtPosition ( forceAccumulator * distribution * 2, attachPointA.position );
            }
            if ( objectB != null ) {
                objectB.AddForceAtPosition ( -forceAccumulator * ( 1 - distribution ) * 2, attachPointB.position );
            }
        }
    }
}
