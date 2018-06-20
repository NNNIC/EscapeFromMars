// This source is created by ExcelStateChartConverter.exe. Source : DebriControl.xlsx
public partial class DebriControl : StateManager {
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
            SetNextState(S_WAIT);
        }
        if (HasNextState())
        {
            GoNextState();
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

        br_Over(S_END);
        if (!HasNextState())
        {
            SetNextState(S_FIRE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_FIRE
        デブリ発射
    */
    void S_FIRE(bool bFirst)
    {
        if (bFirst)
        {
            debri_fire();
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

}
