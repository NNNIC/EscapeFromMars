using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class smoke_unit2 : MonoBehaviour {

    public bool       m_using;

    public float      m_speed=1;
    public float      m_max_y = -5;

    GameObject m_rocket { get { return Globals.stage2_rocket;}}

    [ContextMenu("Kick")]
    public void Kick()
    {
        m_using = true;
        StartCoroutine(move_co());
    }

    private void OnEnable()
    {
        Kick();
    }

    IEnumerator move_co()
    {
        while(m_using)
        { 
            GetComponent<Renderer>().enabled = true;

            var speed_per_frame = m_speed /30f;
            if (m_rocket!=null)
            { 
                gameObject.transform.position = m_rocket.transform.position;       
            }
            while(true)
            {
                var pos = gameObject.transform.localPosition;
                pos.y -= speed_per_frame;
                if (pos.y < m_max_y)
                {
                    break;
                }
                gameObject.transform.localPosition = pos;

                yield return null;
            }
            GetComponent<Renderer>().enabled = false;
        }
    }
}
