using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        Buttons.StartButton_pushed = true;
    }
}
