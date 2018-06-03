using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraAdjust : MonoBehaviour {
    public int m_reference_width
    { get { return (int)Globals.referenceScreenWidth; } }

    public int m_reference_height
    { get { return (int)Globals.referenceScreenHeight; }  }

    public int m_screenwidth;
    public int m_screenheight;

    public static CameraAdjust V;

    void Awake()
    {
        V = this;
    }

    void Start()
    {
        /*
            注：Unity Editor上で実行する際、Start時とOnGUI時のみScreenサイズが正しく取得できる。　　[確認：Unity4.3]
        */
        m_screenwidth = Screen.width;
        m_screenheight = Screen.height;
        Adjust();
    }

    void Adjust()
    {
        float refxy  = (float)m_reference_width / (float)m_reference_height;
        float tgtxy  = (float)m_screenwidth / (float)m_screenheight;

        if (refxy <= tgtxy) 
        {
            GetComponent<Camera>().orthographicSize = m_reference_height / 2.0f;
        }
        else
        {
            float iy = m_screenheight * ((float)m_reference_width) / m_screenwidth ;
            GetComponent<Camera>().orthographicSize = iy / 2;
        }
    }


#if UNITY_EDITOR
    void OnGUI()
    {
        m_screenwidth = Screen.width;
        m_screenheight = Screen.height;
        Adjust();
    }
#endif
}
