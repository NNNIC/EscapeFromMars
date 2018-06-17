// This source is created by ExcelStateChartConverter.exe. Source : MainControl.xlsx
public partial class MainControl : StateManager {
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
            SetNextState(S_DEBUG);
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

}
