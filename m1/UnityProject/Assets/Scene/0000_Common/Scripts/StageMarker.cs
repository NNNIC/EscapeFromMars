using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMarker : MonoBehaviour {

    public GameObject m_man_show;
    public GameObject m_rocket_show;
    public GameObject m_camera_0;
    public GameObject m_camera_1;


	// Use this for initialization
	void Start () {
		Globals.stageMarker = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
