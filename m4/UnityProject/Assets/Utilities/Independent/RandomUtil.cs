using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtil  {

    public static T Choose<T>(List<T> list)
    {
        var i = Random.Range(0,list.Count);
        return list[i];
    }
    public static T Choose<T>(T[] list)
    {
        var i = Random.Range(0,list.Length);
        return list[i];
    }
    

}
