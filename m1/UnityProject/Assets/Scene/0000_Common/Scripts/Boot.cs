using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour　{

    public static Boot V = null;
    public static bool IsReady() { return V!=null && V.m_bReady;   }    

    public static void Create()
    {
        if (V==null)
        {
            if (Globals.staticObject == null)
            {
                var go = new GameObject(Globals.staticGameObjectName);
                GameObject.DontDestroyOnLoad(go);
                Globals.staticObject = go;
            }

            var bootgo = new GameObject("Boot");
            bootgo.transform.parent = Globals.staticObject.transform;

            V = bootgo.AddComponent<Boot>();
        }
    }


    bool m_bReady=false;

    IEnumerator Start()
    {
        Application.targetFrameRate = 30;

        yield return null;

        //共通初期化

        m_bReady = true;
    }
}
