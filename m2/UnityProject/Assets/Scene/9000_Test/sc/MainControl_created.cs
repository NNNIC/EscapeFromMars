//  psggConverterLib.dll converted from MainControl.xlsx. 
public partial class MainControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }


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
        if (!HasNextState())
        {
            SetNextState(S_LAUNCHSTART);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_DEBUG
        デバッグ用
    */
    void S_DEBUG(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_SETUP_STAGE2);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_END
        終了
    */
    void S_END(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_GAMEOVER
        new state
    */
    void S_GAMEOVER(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (HasNextState())
        {
            GoNextState();
        }
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
        if (!HasNextState())
        {
            SetNextState(S_SETUP_STAGE2);
        }
        if (HasNextState())
        {
            GoNextState();
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
        if (!HasNextState())
        {
            SetNextState(S_WAIT);
        }
        if (HasNextState())
        {
            GoNextState();
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
        if (!HasNextState())
        {
            SetNextState(S_WAIT_TIMEOUT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SETUP_STAGE3
        STAGE3をセットアップ
    */
    void S_SETUP_STAGE3(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_START
        開始
    */
    void S_START(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_SETUP_STAGE1);
        }
        if (HasNextState())
        {
            GoNextState();
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
        if (!HasNextState())
        {
            SetNextState(S_CAM_T1);
        }
        if (HasNextState())
        {
            GoNextState();
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
        br_over(S_GAMEOVER);
        br_timeout(S_SETUP_STAGE3);
        if (HasNextState())
        {
            GoNextState();
        }
    }

}

