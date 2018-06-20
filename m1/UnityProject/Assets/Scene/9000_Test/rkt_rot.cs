using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rkt_rot : MonoBehaviour {

    public float m_diff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Globals.rocket_crashed) return;
		transform.Rotate(Vector3.up * m_diff);
	}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.gameObject.name);
    //}

    private void OnTriggerStay(Collider other)
    {
        Globals.rocket_crashed = true;
        //Debug.Log(other.gameObject.name);
    }

}
