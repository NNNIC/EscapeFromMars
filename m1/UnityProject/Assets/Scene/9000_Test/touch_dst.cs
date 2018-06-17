using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch_dst : MonoBehaviour {

    public Camera m_cam;

    TouchDstControl m_tdc;

	// Use this for initialization
	void Start () {
		m_tdc = new TouchDstControl();
        m_tdc.m_touchcam = m_cam;
        
	}
	
	// Update is called once per frame
	void Update () {
		m_tdc.update();
	}
}
