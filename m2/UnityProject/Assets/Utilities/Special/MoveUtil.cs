using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveUtil  {

    public static IEnumerator MoveLarp(GameObject obj, GameObject start, GameObject goal, float t, Action cb)
    {
        var count = t * Application.targetFrameRate;
        for(var i =0; i<=count; i++)
        {
            var pos = Vector3.Lerp(start.transform.position, goal.transform.position, (float)i/count);
            var rot = Quaternion.Lerp(start.transform.rotation, goal.transform.rotation, (float)i/count);
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            yield return null;
        }
        cb();
    }


}
