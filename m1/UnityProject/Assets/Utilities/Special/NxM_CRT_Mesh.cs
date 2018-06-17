using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NxM_CRT_Mesh  {

    public class RawData
    {
        public List<Vector3> verlist  = new List<Vector3>(); // 3D座標
        public List<Vector2> uvlist   = new List<Vector2>(); // 2D座標
        public List<int>     trilist  = new List<int>();     // ポリゴン校正リスト
    }

    /// <summary>
    /// NxNのＣＲＴのメッシュを作成
    /// 
    /// 
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">縦幅</param>
    /// <param name="partlen">パーツ一辺の長さ 1.0未満 </param>
    public static Mesh Create(int width, int height, float partlen)
    {
        var rawdata = new RawData();

        for(var x = 0; x<width; x++)
        {
            for(var y = 0; y<height; y++)
            {
                create_part(x,y,partlen,width,height, ref rawdata);
            }
        }

        var mesh = new Mesh();
        mesh.vertices = rawdata.verlist.ToArray();
        mesh.uv       = rawdata.uvlist.ToArray();
        mesh.triangles = rawdata.trilist.ToArray();

        mesh.RecalculateNormals();

        return mesh;
    }

    private static void create_part(int x,int y,float partlen,int width,int height,ref RawData rawdata)
    {
        /*
            2 - 3 
            | / |
            0 - 1
           o
        */

        var org = new Vector3(- (float)width * 0.5f, -(float)height * 0.5f);

        var  o = org + new Vector3(x,  y);
        var  d = (1.0f - partlen) * 0.5f;

        var v0 = o + new Vector3(d,d);
        var v1 = v0 + new Vector3(partlen,      0);
        var v2 = v0 + new Vector3(      0,partlen);
        var v3 = v0 + new Vector3(partlen,partlen);

        var vlist = new Vector3[4] { v0,v1,v2,v3 };

        var i0 = rawdata.verlist.Count;
        var i1 = i0 + 1;
        var i2 = i0 + 2;
        var i3 = i0 + 3;
#if r
        var tr1 = new int[3] {i0,i1,i3};
        var tr2 = new int[3] {i0,i3,i2};
#else
        var tr1 = new int[3] {i0,i3,i1};
        var tr2 = new int[3] {i0,i2,i3};
#endif

        var ud  = 1.0f / width;
        var udh = ud * 0.5f;
        var vd  = 1.0f / height;
        var vdh = vd * 0.5f;
        
        var uv0 = new Vector2(ud * x + udh, vd * y + vdh);
        var uv1 = uv0;
        var uv2 = uv0;
        var uv3 = uv0;

        var uvlist = new Vector2[4] { uv0, uv1, uv2, uv3 };

        //assemble
        rawdata.verlist.AddRange(vlist);
        rawdata.uvlist.AddRange(uvlist); 
        rawdata.trilist.AddRange(tr1);        
        rawdata.trilist.AddRange(tr2);        
    }
}
