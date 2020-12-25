using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class smoke_unit : MonoBehaviour {

    public bool       m_using;

    public float      m_speed=1;
    public float      m_max_x = 10;

    GameObject m_rocket { get { return Globals.stage1_rocket;}}

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
        var plane_y = -2f;

        while(m_using)
        { 
            GetComponent<Renderer>().enabled = true;

            var speed_per_frame = m_speed /30f;
            if (m_rocket!=null)
            { 
                gameObject.transform.position = m_rocket.transform.position;       
            }
            bool bHorV = false;
            while(true)
            {
                var pos = gameObject.transform.localPosition;
                if (!bHorV)
                {
                    pos.y -= speed_per_frame;
                    if (pos.y < plane_y)
                    {
                        pos.y = plane_y;
                        bHorV = true;
                    }
                }
                else
                {
                    pos.x += speed_per_frame;
                    if (pos.x > m_max_x) break;
                }

                gameObject.transform.localPosition = pos;

                yield return null;
            }
            GetComponent<Renderer>().enabled = false;
        }
    }


}
