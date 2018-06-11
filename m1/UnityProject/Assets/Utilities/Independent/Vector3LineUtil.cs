using UnityEngine;
using System.Collections;

public static class Vector3LineUtil  {

    public class Segment
    {
        public Vector3 pos;
        public Vector3 dir;

        public Segment() { }
        public Segment(Vector3 ipos, Vector3 idir)
        {
            pos = ipos;
            dir = idir;
        }
        public static Segment Create(Vector3 a, Vector3 b)
        {
            return new Segment(a,b-a);
        }
    }

    public enum IntersectResult
    {
        NOTCALC,
        PARALELL,
        INTERSECT_INNER,
        INTERSECT_OUTER,
        NOTINTERSECT
    }

    //http://www.sousakuba.com/Programming/gs_two_lines_intersect.html
    public static IntersectResult Intersect(Segment s1, Segment s2, out Vector3 p1, out Vector3 p2)
    {   
        p1 = p2 = Vector3.zero;

        if (s1.dir.magnitude == 0 || s2.dir.magnitude == 0) return IntersectResult.NOTCALC;

        //if ( Vector3.Cross(s1.dir,s2.dir).magnitude < 0.0001f) return IntersectResult.PARALELL; 

        var n1 = s1.dir.normalized;
        var n2 = s2.dir.normalized;
        var work1 = Vector3.Dot(n1,n2); 
        var work2 = 1 - work1 * work1;
        if (work2 == 0) return IntersectResult.NOTCALC;

        var vAC = s2.pos - s1.pos;
        var d1 = ( Vector3.Dot(vAC,n1) - work1 * Vector3.Dot(vAC,n2)) / work2;
        var d2 = ( work1 * Vector3.Dot(vAC,n1) - Vector3.Dot(vAC,n2)) / work2;

        var p1_len = (d1*n1).magnitude;
        p1 = s1.pos + d1 * n1;

        var p2_len = (d2*n2).magnitude;
        p2 = s2.pos + d2 * n2;

        var diff = p2 - p1;
        if (diff.magnitude <= 0.01f)
        {
            if (p1_len < s1.dir.magnitude && p2_len < s2.dir.magnitude)
            {
                return IntersectResult.INTERSECT_INNER;
            }
            else
            {
                return IntersectResult.INTERSECT_OUTER;
            }
        }
        return IntersectResult.NOTINTERSECT;
    }

    //public static bool  CalcCircle(Vector3 p1, Vector3 p2, Vector3 p3)
    //{
    //    Vector3 t = p2-p1;
    //    Vector3 u = p3-p1;
    //    Vector3 v = p3-p2;

    //    Vector3 w = Vector3.Cross(t,u);
    //    float wsl = w.magnitude;
    //    if (wsl<Mathf.Epsilon) return false;

    //    float iwsl2 = 1.0f / (2.0f*wsl);
    //    float tt = Vector3.Dot();



    //}

    public static bool CalcCircumscribedCircle(Vector3 rA, Vector3 rB, Vector3 rC, out Vector3 center, out float radius) 
    {
        //http://ja.wikipedia.org/wiki/%E5%A4%96%E6%8E%A5%E5%86%86
        var a = rB - rA;
        var b = rC - rB;
        var c = rA - rC;
        var A = a.sqrMagnitude;
        var B = b.sqrMagnitude;
        var C = c.sqrMagnitude;
        
        var S = Vector3.Cross(a,b).magnitude / 2;
        center = (A *(B+C-A)*rA + B*(C+A-B)*rB + C*(A+B-C)*rC ) / (16f * S * S );

        radius = (a-center).magnitude;

        //Debug.Log("radius = " +radius + "," + (b-center).magnitude + "," + (c-center).magnitude );

        return true;
    }

    public static float GetAngle(Vector3 center, Vector3 a, Vector3 b)
    {
        /*     b
              /
             /
            c ---- a           
        */
        Vector3 vca = a - center;
        Vector3 vcb = b - center;

        //var cross = Cross(vca, vcb);
        //var dot = Vector2.Dot(vca, vcb);

        //return Mathf.Atan2(cross, dot);

        return Vector3.Angle(vca,vcb);
    }
}
