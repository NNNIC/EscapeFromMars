using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class DebriControl  {
	
    public debri_fire_controll m_dfc;

    public void Start()
    {
        Goto(S_START);
    }

    bool m_timer_done;
    void set_timer()
    {
        m_timer_done = false;
        m_dfc.StartCoroutine(timer_co(m_dfc.m_waittime));

    }
    IEnumerator timer_co(float time)
    {
        yield return new WaitForSeconds(time);
        m_timer_done = true;
    }
    bool timer_done() { return m_timer_done;}

    void br_Over(Action<bool> st)
    {
        if (Globals.rocket_crashed)
        {
            SetNextState(st);
        }
    }

    void debri_fire()
    {
        //var list = m_dfc.m_stage2.m_debris_m;
        var go = listFind();// list.Find(i=>i.gameObject.activeSelf==false);
        if (go==null)
        {
            return;
        }
        go.SetActive(true);

        var fp = RandomUtil.Choose(m_dfc.m_stage2.m_ftops);
        go.transform.position = fp.transform.position;
        var dm = go.GetComponent<debri_move>();
        dm.m_diff = Vector3.down * m_dfc.m_speed;
    }

    int m_index = 0;
    GameObject listFind() //Rotation Find!
    {
        var list = m_dfc.m_stage2.m_debris_m;
        for(var i = 0; i<list.Count; i++)
        {
            var n = (i + m_index) % list.Count;
            var o =list[n];
            if (o.activeSelf==false)
            {
                m_index = n;
                return o;
            }
        }
        return null;
    }
}
