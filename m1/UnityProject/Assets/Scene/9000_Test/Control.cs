using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    public Camera  m_maincamper;
    public Camera  m_maincamort;
    public GameObject m_fadeort;
    public GameObject m_gameover;
    public stage_1 m_stage_1;
    public stage_2 m_stage_2;

    public MainControl m_maincontrol;

	void Start () {

        Application.targetFrameRate = 30;

        m_maincontrol = new MainControl();		
        m_maincontrol.m_control = this;
        m_maincontrol.m_stage_1 = m_stage_1;
        m_maincontrol.m_stage_2 = m_stage_2;
        m_maincontrol.m_maincamper = m_maincamper;
        m_maincontrol.m_maincamort = m_maincamort;
        m_maincontrol.m_fadeort    = m_fadeort;
        m_maincontrol.m_gameover   = m_gameover;
        
        m_maincontrol.Start();
	}
	
	void Update () {
        m_maincontrol.update();		
	}
}
