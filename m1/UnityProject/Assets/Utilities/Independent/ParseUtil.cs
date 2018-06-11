using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseUtil  {

    public static Vector2 Vector2Parse(string s)
    {
        var v = s.Replace("(","").Replace(")","");
        var list = FloatListParse(v);
        if (list == null || list.Count!=2)
        {
            throw new System.Exception("Unknown");
        }
        return new Vector2(list[0],list[1]);
    }
    
    public static List<Vector2> Vector2ListParse(string s)
    {
        var list = new List<Vector2>();
        var tokens = s.Split(',');

        string vs = null;
        foreach(var i in tokens)
        {
            if (string.IsNullOrEmpty(i) || string.IsNullOrEmpty(i.Trim()))
            {
                throw new System.Exception();
            }
            var ni = i.Trim();

            if (vs==null)
            {
                if (ni[0]=='(')
                {
                    vs = ni;
                    continue;
                }
            }
            else
            {
                if (ni[ni.Length-1]==')')
                {
                    vs += "," + ni;
                    var v2 = Vector2Parse(vs);
                    list.Add(v2);
                    vs = null;
                }
                else
                {
                    throw new System.Exception();
                }
            }
        } 
        if (vs!=null)
        {
            throw new System.Exception();
        }

        return list;
    }
    public static List<float> FloatListParse(string s)
    {
        var list = new List<float>();
        var tokens = s.Split(',');
        foreach(var i in tokens)
        {
            var ns = i.Trim();
            if (ns==null)
            {
                Debug.LogWarning("floatListParse faild #1 :" + s);
                continue;
            }
            float f = 0;
            if (float.TryParse(ns,out f))
            {
                list.Add(f);
            }
            else
            {
                Debug.LogWarning("floatListParse faild #2 :" + s);
                continue;
            }
        }
        return list;
    }
}
