using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Touch2Control : MonoBehaviour {
 
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

	#region    // [PSGG OUTPUT START] indent(4) $/./$
//  psggConverterLib.dll converted from Touch2Control.xlsx.    psgg-file:doc\Touch2Control.psgg
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_INIT
        初期化
    */
    void S_INIT(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_WAIT_TOUCH);
        }
    }
    /*
        S_MOVE_DST
        DST移動
    */
    void S_MOVE_DST(bool bFirst)
    {
        if (bFirst)
        {
            dst_move(0.1f);
        }
        if (!dst_done()) return;
        //
        if (!HasNextState())
        {
            Goto(S_WAIT_TOUCH);
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_INIT);
        }
    }
    /*
        S_WAIT_TOUCH
        タッチ待ち
    */
    void S_WAIT_TOUCH(bool bFirst)
    {
        if (bFirst)
        {
            touch_start();
        }
        touch_update();
        if (!touch_done()) return;
        //
        if (!HasNextState())
        {
            Goto(S_MOVE_DST);
        }
    }


	#endregion // [PSGG OUTPUT END]

    public Camera m_touchcam;
    public Camera m_orhcam;
    public Vector3? m_dst_pos;
    public GameObject m_dstmark;
    public GameObject m_rkt;
    public Texture m_tex;

    public float      m_dst_speed = 10;

    public float     m_screen_width;
    public float     m_screen_height;
    public float     m_width;
    public float     m_height;

    public MonoBehaviour m_mono;

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

	// write your code below

	bool m_bYesNo;
	
	void br_YES(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (m_bYesNo)
			{
				Goto(st);
			}
		}
	}

	void br_NO(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (!m_bYesNo)
			{
				Goto(st);
			}
		}
	}

    #region Monobehaviour framework
    void Start()
    {
        m_screen_width  = Screen.width;
        m_screen_height = Screen.height;

        m_width  = m_tex.width;
        m_height = m_tex.height;

        m_mono = this;

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

