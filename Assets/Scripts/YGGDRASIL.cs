using System;
using UnityEngine;

public class DataLinkNTF : IComparable {
    public  string      content = " !!! ";
    public  float       timeout = 5;            // If timeout = -1 then it stays on
    public  int         prio = 0;
    public DataLinkNTF ( string _content = " !!! ", float _timeout = -1, int _prio = 0 ) {
        content = _content;
        timeout = _timeout;
        prio = _prio;
    }

    public int CompareTo ( object obj ) {
        DataLinkNTF other = obj as DataLinkNTF;
        if ( prio == other.prio ) return 0;
        if ( prio == -1 ) return -1;
        if ( other.prio == -1 ) return 1;
        if ( prio > other.prio ) return -1;
        return 1;
    }
}

public enum ItemPolarity { Item, Weapon, Equipment };

public static class ItemPolarityChecker {
    // COMPATIBILITY CHECK
    public  static  bool    CPC ( ItemPolarity port, ItemPolarity item ) {
        if ( port == ItemPolarity.Item ) return true;
        if ( port == item ) return true;
        return false;
    }
    // TAG FROM POLARITY
    public  static  string  TFP ( ItemPolarity a ) {
        switch ( a ) {
            case ( ItemPolarity.Weapon ): return "W";
            case ( ItemPolarity.Equipment ): return "E";
            case ( ItemPolarity.Item ): return "I";
            default: return "X";
        }
    } 
}

public class PayloadObject {
    public  Vector2     hostV;
    public  Vector2     heading;
    public  Transform   target;
    public  Transform   controllerRoot;
    public  float       expectedLifetime;
    public PayloadObject () {
        hostV = Vector2.zero;
        heading = Vector2.up;
        target = null;
        controllerRoot = null;
        expectedLifetime = -1;
    }
    public PayloadObject ( Vector2 _hostV, Vector2 _heading, Transform _target = null, Transform _controllerRoot = null, float _expectedLifetime = -2 ) {
        hostV = _hostV;
        heading = _heading;
        target = _target;
        expectedLifetime = _expectedLifetime;
        controllerRoot = _controllerRoot;
    }
    public void InjectCR ( Transform alpha ) {
        controllerRoot = alpha;
    }
    public override string ToString () {
        return hostV + " " + heading + " " + target + " " + controllerRoot + " " + expectedLifetime;
    }
}

public class LineIntersection {
    public static Vector2 GetIntersectionPoint ( Vector2 startPoint1, Vector2 direction1, Vector2 startPoint2, Vector2 direction2 ) {
        Vector2 startPointDelta = startPoint2 - startPoint1;
        float cross = direction1.x * direction2.y - direction1.y * direction2.x;

        if ( Mathf.Approximately ( cross, 0 ) ) {
            // Lines are parallel or collinear, check if they overlap
            float denominator = direction1.x * direction2.y - direction1.y * direction2.x;
            if ( Mathf.Approximately ( denominator, 0 ) ) {
                // Lines are collinear, check if they overlap
                float t0 = Vector2.Dot((startPoint2 - startPoint1), direction1) / Vector2.Dot(direction1, direction1);
                float t1 = t0 + Vector2.Dot(direction2, direction1) / Vector2.Dot(direction1, direction1);
                if ( ( t0 >= 0 && t0 <= 1 ) || ( t1 >= 0 && t1 <= 1 ) ) {
                    // Lines overlap, return the overlapping segment
                    float tMin = Mathf.Max(0, Mathf.Min(t0, t1));
                    float tMax = Mathf.Min(1, Mathf.Max(t0, t1));
                    Vector2 overlapStartPoint = startPoint1 + direction1 * tMin;
                    Vector2 overlapEndPoint = startPoint1 + direction1 * tMax;
                    return ( overlapStartPoint + overlapEndPoint ) / 2;
                }
            }
            return Vector2.zero; // Lines are parallel or do not overlap
        }

        float t = (startPointDelta.x * direction2.y - startPointDelta.y * direction2.x) / cross;
        float u = (startPointDelta.x * direction1.y - startPointDelta.y * direction1.x) / cross;

        if ( t >= 0 && t <= 1 && u >= 0 && u <= 1 ) {
            Vector2 intersectionPoint = startPoint1 + direction1 * t;
            return intersectionPoint;
        }

        return Vector2.zero; // Lines do not intersect within the given line segments
    }
}

public class DamageLink {
    public  int     damage;
}