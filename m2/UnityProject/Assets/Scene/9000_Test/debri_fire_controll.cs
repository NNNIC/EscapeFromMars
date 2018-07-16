using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debri_fire_controll : MonoBehaviour {

    public float   m_waittime = 1;
    public float   m_speed = 0.1f;
    public stage_2 m_stage2;

    public DebriControl m_dc;

	// Use this for initialization
	void Start () {
		m_dc = new DebriControl();
        m_dc.m_dfc = this;
        m_dc.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_dc!=null) m_dc.update();
	}
}
