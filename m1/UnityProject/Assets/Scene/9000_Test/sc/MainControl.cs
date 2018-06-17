using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public partial class MainControl {

    public Camera  m_maincamper;
    public Camera  m_maincamort;
    public stage_1 m_stage_1;
    public stage_2 m_stage_2;

    public void Start()
    {
        this.Goto(S_START);
    }

    // S_SETUP
    void stage_1_set()
    {
        m_stage_1.gameObject.SetActive(true);
    }
    void camper_set()
    {
        m_maincamper.gameObject.SetActive(true);
        m_maincamort.gameObject.SetActive(false);
    }
    void cam_set_t0()
    {
        m_maincamper.transform.position = m_stage_1.m_camera_0001.transform.position;
    }

    // S_WAIT
    bool m_timer_done;
    void wait_timer(float sec)
    {
        m_timer_done = false;
        m_stage_1.StartCoroutine(wait_timer_co(sec));
    }
    IEnumerator wait_timer_co(float sec)
    {
        yield return new WaitForSeconds(sec);
        m_timer_done = true;
    }
    bool wait_timer_done()
    {
        return m_timer_done;
    }
    // S_CAM_T1
    bool m_cam_done;
    void cam_move_t1(float t)
    {
        m_cam_done = false;
        m_stage_1.StartCoroutine(MoveUtil.MoveLarp(m_maincamper.gameObject, m_stage_1.m_camera_0001,m_stage_1.m_camera_0002, t, ()=> { m_cam_done = true; }));
    }
    bool cam_move_t1_done()
    {
        return m_cam_done;
    }
    //S_LAUNCHSTART
    bool m_rkt_done;
    void rkt_launch()
    {
        m_rkt_done = false;
        m_stage_1.StartCoroutine(rkt_launch_co());
    }
    IEnumerator rkt_launch_co()
    {
        // 参照：https://keisan.casio.jp/exec/system/1180917567
        // 高度
        // ゆっくり上り、だんだん加速 
        // f(x) = x^10 + x^5 + 0.2x   f(1)=2.2
        Func<double,double> fh = (t)=> {
            return Math.Pow(t,10f) + Math.Pow(t,5f) + 0.2f *t;
        };

        var goal_y = m_stage_1.m_r_1.transform.position.y;        
        var start_y = m_stage_1.m_r_0.transform.position.y;
        var dy = (double)(goal_y - start_y) / 2.2f;

        // 回転
        // ゆっくり回転初めて、しばらくすると逆回転へ
        // f(x)=(0.2 + 0.8*x )*sin(x*x*x) -0.3* sin(x*1.4)
        //  min = -0.15 max = 0.6   0.75
        Func<double,double> fa = (t)=> {
            return (0.2f + 0.8f * t) * Math.Sin(t) - 0.3f * Math.Sin(t * 1.4f);
        };        
        var rotsize = 90f;
        var da      = rotsize /0.75f;

        var span = 10.0f;
        var maxframes = span * Application.targetFrameRate;
        for(var i = 0; i<maxframes; i++)
        {
            var t = (double)i / maxframes;
            var h = fh(t) * dy;
            m_stage_1.m_rocket.transform.position = VectorUtil.Mod_Y(m_stage_1.m_rocket.transform.position, (float)h);

            var a = fa(t) * da;
            m_stage_1.m_rocket.transform.eulerAngles = new Vector3(0,(float)a,0);

            yield return null;
        }
        m_rkt_done = true;
    }
    bool rkt_launch_done() { return m_rkt_done;}

    // S_SETUP_STAGE2
    void stage_2_set()
    {
        m_stage_1.gameObject.SetActive(false);
        m_stage_2.gameObject.SetActive(true);        
    }
    void camort_set()
    {
        m_maincamort.gameObject.SetActive(true);
    }
    void cam_set_o1()
    {
       
    }
}
