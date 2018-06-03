using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMove : MonoBehaviour {

    float m_speed = 10;
    bool  m_bDoneMoving = false;

    void Awake()
    {
        Globals.manMove = this;
        gameObject.SetActive(false);
    }

    public void Move(float speed = 0)
    {
        if (speed!=0) m_speed = speed;
        m_bDoneMoving = false;
        StartCoroutine(move());
    }

    public bool IsMoveDone()
    {
        return m_bDoneMoving;
    }

    IEnumerator move() {
        var start = Globals.stageMarker.m_man_show.transform.position;
        var goal  = Globals.stageMarker.m_rocket_show.transform.position;
        
        transform.position = start;

        var len = (start - goal).magnitude;
        var count = len / m_speed *30f;
        for(var i = 0; i<count+1; i++)
        {
            yield return null;
            var s = Mathf.Clamp01(1.0f/count * i);
            transform.position = Vector3.Lerp(start,goal,s);
        }
        m_bDoneMoving = true;
	}
}
