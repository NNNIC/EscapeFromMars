using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour {

    BaseControl m_basecontrol;

	// Use this for initialization
	void Start () {
		m_basecontrol = new BaseControl();
        m_basecontrol.Start();
	}
	
	// Update is called once per frame
	void Update () {
		m_basecontrol.update();
	}
}
