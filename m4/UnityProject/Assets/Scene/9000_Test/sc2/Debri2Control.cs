using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Debri2Control : MonoBehaviour {
 
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
//  psggConverterLib.dll converted from Debri2Control.xlsx.    psgg-file:doc\Debri2Control.psgg
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_FIRE
        デブリ発射
    */
    void S_FIRE(bool bFirst)
    {
        if (bFirst)
        {
            Debug.Log("*psgg-trace:S_FIRE*");
            debri_fire();
        }
        //
        if (!HasNextState())
        {
            Goto(S_WAIT);
        }
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
            Goto(S_WAIT);
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
        S_WAIT
        待ち
    */
    void S_WAIT(bool bFirst)
    {
        if (bFirst)
        {
            set_timer();
        }
        if (!timer_done()) return;
        // branch
        if (Globals.rocket_crashed) { Goto( S_END ); }
        else { Goto( S_FIRE ); }
    }


	#endregion // [PSGG OUTPUT END]

	// write your code below

    //public debri_fire_controll m_dfc;
    public float   m_waittime = 1;
    public float   m_speed = 0.1f;
    public stage_2 m_stage2;


    bool m_timer_done;
    void set_timer()
    {
        m_timer_done = false;
        StartCoroutine(timer_co(m_waittime));

    }
    IEnumerator timer_co(float time)
    {
        yield return new WaitForSeconds(time);
        m_timer_done = true;
    }
    bool timer_done() { return m_timer_done;}

    //void br_Over(Action<bool> st)
    //{
    //    if (Globals.rocket_crashed)
    //    {
    //        SetNextState(st);
    //    }
    //}

    void debri_fire()
    {
        //var list = m_dfc.m_stage2.m_debris_m;
        var go = listFind();// list.Find(i=>i.gameObject.activeSelf==false);
        if (go==null)
        {
            return;
        }
        go.SetActive(true);

        var fp = RandomUtil.Choose(m_stage2.m_ftops);
        go.transform.position = fp.transform.position;
        var dm = go.GetComponent<debri_move>();
        dm.m_diff = Vector3.down * m_speed;
    }

    int m_index = 0;
    GameObject listFind() //Rotation Find!
    {
        var list = m_stage2.m_debris_m;
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


	//bool m_bYesNo;
	
	//void br_YES(Action<bool> st)
	//{
	//	if (!HasNextState())
	//	{
	//		if (m_bYesNo)
	//		{
	//			Goto(st);
	//		}
	//	}
	//}

	//void br_NO(Action<bool> st)
	//{
	//	if (!HasNextState())
	//	{
	//		if (!m_bYesNo)
	//		{
	//			Goto(st);
	//		}
	//	}
	//}

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

