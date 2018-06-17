// This source is created by ExcelStateChartConverter.exe. Source : TouchDstControl.xlsx
public partial class TouchDstControl : StateManager {
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
            SetNextState(S_INIT);
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
        S_INIT
        初期化
    */
    void S_INIT(bool bFirst)
    {
        if (bFirst)
        {

        }



        if (!HasNextState())
        {
            SetNextState(S_WAIT_TOUCH);
        }
        if (HasNextState())
        {
            GoNextState();
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


        if (!HasNextState())
        {
            SetNextState(S_MOVE_DST);
        }
        if (HasNextState())
        {
            GoNextState();
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
            dst_move();
        }

        if (!dst_done()) return;


        if (!HasNextState())
        {
            SetNextState(S_WAIT_TOUCH);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
