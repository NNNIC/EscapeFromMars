using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debri_move : MonoBehaviour {

    public Vector3 m_diff;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    transform.position += m_diff;
        if (m_diff.y < 0)
        {
            if (transform.position.y < -35)
            {
                gameObject.SetActive(false);
            }
        }
        if (m_diff.x < 0)
        {
            if (transform.position.y < -25)
            {
                gameObject.SetActive(false);
            }
        }
        if (m_diff.x > 0)
        {
            if (transform.position.y > 25)
            {
                gameObject.SetActive(false);
            }
        }
	}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.name);
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log(other.gameObject.name);
    //}
}
