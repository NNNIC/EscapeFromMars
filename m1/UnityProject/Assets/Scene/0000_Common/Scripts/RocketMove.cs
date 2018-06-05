using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Globals.rocketMove = this;
	}

    float m_speed;
    Vector3 m_start;
    Vector3 m_goal;
    bool m_bDoneMove;	
   
	// Update is called once per frame
   public void Move(Vector3 start, Vector3 goal,float speed=0)
    {
        if (speed != 0)
        {
            m_speed = speed;
        }

        m_start      = start;
        m_goal       = goal;
        m_bDoneMove = false;

        StartCoroutine(move());
    }

    public bool IsMoveDone()
    {
        return m_bDoneMove;
    }

    IEnumerator move()
    {
        transform.position = m_start;
        var len = (m_start - m_goal).magnitude;
        var count = len / m_speed * 30f;
        for(var i = 0; i<count+1;i++)
        {
            yield return null;
            var s= Mathf.Clamp01(1.0f/count * i);
            transform.position = Vector3.Lerp(m_start,m_goal,s);
        }
        m_bDoneMove = true;
    }
}
