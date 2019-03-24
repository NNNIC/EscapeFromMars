using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Main2Control : MonoBehaviour {
 
    #region manager
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;

    bool         m_noWait;
    
    void _update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    bool HasNextState()
    {
        return m_nextfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion
    #region gosub
    List<Action<bool>> m_callstack = new List<Action<bool>>();
    void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
    {
        m_callstack.Insert(0,returnstate);
        Goto(nextstate);
    }
    void ReturnState()
    {
        var nextstate = m_callstack[0];
        m_callstack.RemoveAt(0);
        Goto(nextstate);
    }
    #endregion 

    void _start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }

	#region    // [PSGG OUTPUT START] indent(8) $/./$
//  psggConverterLib.dll converted from Main2Control.xlsx. 
        /*
            S_CAM_T1
            カメラを発射正面へ
        */
        void S_CAM_T1(bool bFirst)
        {
            if (bFirst)
            {
                cam_move_t1(5);
            }
            if (!cam_move_t1_done()) return;
            //
            if (!HasNextState())
            {
                Goto(S_LAUNCHSTART);
            }
        }
        /*
            S_DEBUG
            デバッグ用
        */
        void S_DEBUG(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_SETUP_STAGE2);
            }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_GAMEOVER
            new state
        */
        void S_GAMEOVER(bool bFirst)
        {
        }
        /*
            S_LAUNCHSTART
            発射スタート
        */
        void S_LAUNCHSTART(bool bFirst)
        {
            if (bFirst)
            {
                rkt_launch();
            }
            if (!rkt_launch_done()) return;
            //
            if (!HasNextState())
            {
                Goto(S_SETUP_STAGE2);
            }
        }
        /*
            S_SETUP_STAGE1
            STAGE1セットアップ
        */
        void S_SETUP_STAGE1(bool bFirst)
        {
            if (bFirst)
            {
                stage_1_set();
                camper_set();
                cam_set_t0();
            }
            //
            if (!HasNextState())
            {
                Goto(S_WAIT);
            }
        }
        /*
            S_SETUP_STAGE2
            STAGE2をセットアップ
        */
        void S_SETUP_STAGE2(bool bFirst)
        {
            if (bFirst)
            {
                stage_2_set();
                camort_set();
                cam_set_o1();
            }
            //
            if (!HasNextState())
            {
                Goto(S_WAIT_TIMEOUT);
            }
        }
        /*
            S_SETUP_STAGE3
            STAGE3をセットアップ
        */
        void S_SETUP_STAGE3(bool bFirst)
        {
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_SETUP_STAGE1);
            }
        }
        /*
            S_WAIT
            ３秒待ち
        */
        void S_WAIT(bool bFirst)
        {
            if (bFirst)
            {
                wait_timer(3);
            }
            if (!wait_timer_done()) return;
            //
            if (!HasNextState())
            {
                Goto(S_CAM_T1);
            }
        }
        /*
            S_WAIT_TIMEOUT
            タイムアウトを待つ。死亡時もあり。
        */
        void S_WAIT_TIMEOUT(bool bFirst)
        {
            if (bFirst)
            {
                timer_reset();
            }
            timer_update();
            if (!wait_timerdone_or_over(15)) return;
            // branch
            if (Globals.rocket_crashed) { Goto( S_GAMEOVER ); }
            else if (m_timeout) { Goto( S_SETUP_STAGE3 ); }
        }


	#endregion // [PSGG OUTPUT END]

	// write your code below

    public Camera  m_maincamper;
    public Camera  m_maincamort;
    public GameObject m_fadeort;
    public GameObject m_gameover;
    public stage_1 m_stage_1;
    public stage_2 m_stage_2;

        // S_SETUP
    void stage_1_set()
    {
        Globals.rocket_crashed = false;
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
        StartCoroutine(wait_timer_co(sec));
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
        StartCoroutine(MoveUtil.MoveLarp(m_maincamper.gameObject, m_stage_1.m_camera_0001,m_stage_1.m_camera_0002, t, ()=> { m_cam_done = true; }));
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
        StartCoroutine(rkt_launch_co());
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
        var f = Application.targetFrameRate > 0 ? Application.targetFrameRate : 30;
        var maxframes = span * f;
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
    // S_WAIT_TIMEOUT
    float m_timer;
    bool m_timeout;
    void timer_reset()
    {
        m_timeout = false;
        m_timer = 0;
    }
    void timer_update()
    {
        m_timer += Time.deltaTime;
    }
    bool wait_timerdone_or_over(float time)
    {
        if (Globals.rocket_crashed)
        {
            m_fadeort.SetActive(true);
            m_gameover.SetActive(true);
            return true;
        }
        if (m_timer > time) {
            m_timeout = true;
            return true;
        }
        return false;
    }
    void br_timeout(Action<bool> st)
    {
        if (m_timeout)
        {
            Goto(st);
        }
    }
    void br_over(Action<bool> st)
    {
        if (Globals.rocket_crashed)
        {
            Goto(st);
        }
    }

    #region Monobehaviour framework
    void Start()
    {
        _start();
    }
    void Update()
    {
        if (!IsEnd()) 
        {
            _update();
        }
    }
    #endregion
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

