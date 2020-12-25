using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test0001 : MonoBehaviour {

    public Material  m_mat;
    public Camera    m_cam;
    public RenderTexture m_tex;
    public float     m_pixelsize= 0.1f;
    public int       m_width  { get { return m_tex.width;  } }
    public int       m_height { get { return m_tex.height; } }
            

    IEnumerator Start()
    {
        var width  = m_width;
        var height = m_height;

        var mesh = NxM_CRT_Mesh.Create(width,height,m_pixelsize);
        var gobj = new GameObject("crt");
        gobj.layer = 10;
        gobj.transform.parent = transform.parent;
        gobj.transform.localPosition = Vector3.zero;
        var rend   = gobj.AddComponent<MeshRenderer>();
        var filter = gobj.AddComponent<MeshFilter>();
        filter.mesh = mesh;
        
        //m_tex = new Texture2D(width,height,TextureFormat.RGBA32,false);

        m_mat.SetTexture("_MainTex",m_tex);
        
        rend.material = m_mat;

        m_cam.orthographic = true;
        m_cam.orthographicSize = (float)height * 0.5f;
        m_cam.transform.localPosition = new Vector3(0,0,-10);

        yield return null;

        //for(var y = 0; y< height; y++)
        //{
        //    for(var x = 0; x<width; x++)
        //    {
        //        tex.SetPixel(x,y,Color.black);
        //        tex.Apply();
        //        yield return null;
        //    }

        //}

    }
}
