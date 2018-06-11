using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUtil {

    private static Camera __camera;
    private static Camera m_camera {
        get {
            if (__camera==null)
            {
                //var go = GameObject.Find("/Hud/Hud Camera");
                __camera = Camera.main; //go.GetComponent<Camera>();
            }
            return __camera;
        }
    }
    public static Vector3 ScreenPosition2WorldPosition(Vector3 spos)
    {
        return m_camera.ScreenToWorldPoint(spos);
    }
}
