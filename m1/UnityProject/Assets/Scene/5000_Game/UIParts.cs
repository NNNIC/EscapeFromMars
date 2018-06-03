using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParts : MonoBehaviour {

    public Button m_startbutton;

	// Use this for initialization
	void Start () {
		Buttons.StartButton = m_startbutton;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
