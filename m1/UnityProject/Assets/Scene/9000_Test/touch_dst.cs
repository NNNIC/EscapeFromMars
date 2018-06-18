using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch_dst : MonoBehaviour {

    public Camera m_cam;
    public Camera m_ort;
    public Texture m_tex;
    public GameObject m_rocket;


    TouchDstControl m_tdc;

	// Use this for initialization
	void Start () {
		m_tdc = new TouchDstControl();
        m_tdc.m_touchcam      = m_cam;
        m_tdc.m_orhcam        = m_ort;
        m_tdc.m_dstmark       = gameObject;
        m_tdc.m_rkt           = m_rocket;
        m_tdc.m_screen_width  = Screen.width;
        m_tdc.m_screen_height = Screen.height;
        m_tdc.m_width         = m_tex.width;
        m_tdc.m_height        = m_tex.height;

        m_tdc.m_mono          = this;

        m_tdc.Start();        
	}
	
	// Update is called once per frame
	void Update () {
		m_tdc.update();
	}
}
