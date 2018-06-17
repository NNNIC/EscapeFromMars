using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TouchDstControl  {
    public Camera m_touchcam;
    public Vector3? m_dst_pos;
    public GameObject m_rkt;
    public float      m_dst_speed = 10;

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
        if (Input.touchCount==0) return;
        var spos = Input.mousePosition;
        var wpos = m_touchcam.ScreenToWorldPoint(spos);
        m_dst_pos = VectorUtil.Mod_X(m_rkt.transform.position, wpos.x);
    }
    bool touch_done()
    {
        return m_dst_pos!=null;
    }
    void dst_move()
    {

    }
    bool dst_done()
    {
        return true;
    }
}
