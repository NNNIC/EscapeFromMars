using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TouchDstControl  {
    public Camera m_touchcam;
    public Camera m_orhcam;
    public Vector3? m_dst_pos;
    public GameObject m_dstmark;
    public GameObject m_rkt;
    public float      m_dst_speed = 10;

    public float     m_screen_width;
    public float     m_screen_height;
    public float     m_width;
    public float     m_height;

    public MonoBehaviour m_mono;

    public void Start()
    {
        Goto(S_START);
    }
    
    void touch_start()
    {
        m_dst_pos = null;
    }
    void touch_update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            var h = m_orhcam.orthographicSize * 2;

            var spos = Input.mousePosition;
            var x = (spos.x - m_screen_width * 0.5f)  * (h/m_screen_height);
                
            m_dst_pos = VectorUtil.Mod_X(m_rkt.transform.position, x);
        }

    }
    bool touch_done()
    {
        return m_dst_pos!=null;
    }

    bool m_move_done;
    void dst_move(float speed = 1)
    {
        m_move_done = false;
        m_dstmark.transform.position = (Vector3)m_dst_pos;
        m_dstmark.GetComponent<Renderer>().enabled = true;
        m_mono.StartCoroutine(dst_move_co(speed));
    }

    IEnumerator dst_move_co(float speed)
    {
        while(true)
        {
            if (Globals.rocket_crashed) yield break;
            var len = (m_rkt.transform.position -  m_dstmark.transform.position).magnitude;
            if (len < speed)
            {
                m_rkt.transform.position = m_dstmark.transform.position;
                m_dstmark.GetComponent<Renderer>().enabled = false;

                m_move_done = true;
                yield break;
            }
            else
            {
                var v = m_dstmark.transform.position - m_rkt.transform.position;
                if (v.x > 0)
                {
                    m_rkt.transform.position += Vector3.right * speed; 
                }
                else
                {
                    m_rkt.transform.position -= Vector3.right * speed; 
                }
            }
            yield return null;
        }
    }

    bool dst_done()
    {
        return m_move_done;
    }
}
