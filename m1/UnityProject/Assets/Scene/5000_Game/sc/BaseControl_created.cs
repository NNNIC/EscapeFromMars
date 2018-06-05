// This source is created by ExcelStateChartConverter.exe. Source : StateControl.xlsx
public partial class BaseControl : StateManager {
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
            SetNextState(S_BOOTSCENE);
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
        S_MENUSCENE
        メニューシーン
    */
    void S_MENUSCENE(bool bFirst)
    {
        if (bFirst)
        {
            show_startbutton();
        }

        if (!wait_for_startbutton()) return;
        hide_startbutton();

        if (!HasNextState())
        {
            SetNextState(S_ROCKETSHOW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_GAMESCENE
        ゲームシーン
    */
    void S_GAMESCENE(bool bFirst)
    {
        if (bFirst)
        {

        }



        if (!HasNextState())
        {
            SetNextState(S_GAMEOVERSCENE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_GAMEOVERSCENE
        ゲームオーバーシーン
    */
    void S_GAMEOVERSCENE(bool bFirst)
    {
        if (bFirst)
        {

        }



        if (!HasNextState())
        {
            SetNextState(S_END);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_BOOTSCENE
        ブートシーン
    */
    void S_BOOTSCENE(bool bFirst)
    {
        if (bFirst)
        {
            boot();
        }



        if (!HasNextState())
        {
            SetNextState(S_MENUSCENE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_ROCKETSHOW
        ロケット出現
    */
    void S_ROCKETSHOW(bool bFirst)
    {
        if (bFirst)
        {
                m_counter=0;

        }

        if (m_counter++<30) return;
        rocket_show();

        if (!HasNextState())
        {
            SetNextState(S_MANMOVE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_MANMOVE
        人間出現＆移動
    */
    void S_MANMOVE(bool bFirst)
    {
        if (bFirst)
        {
            mankind_show_and_move(10.0f);
        }

        if (!mankind_move_done()) return;


        if (!HasNextState())
        {
            SetNextState(S_ZOOMOUT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_ZOOMOUT
        ズームアウト
    */
    void S_ZOOMOUT(bool bFirst)
    {
        if (bFirst)
        {
            camera_zoom_at_launch(20);
        }

        if (!camera_zoom_at_launch_done()) return;


        if (!HasNextState())
        {
            SetNextState(S_LAUNCH);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_LAUNCH
        発射
    */
    void S_LAUNCH(bool bFirst)
    {
        if (bFirst)
        {
            rocket_launch(100);
        }

        if (!rocket_launch_done()) return;


        if (!HasNextState())
        {
            SetNextState(S_GAMESCENE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
