﻿public partial class BaseControl  {
	// write your code 
    
    int m_counter;

    public void Start()
    {
        Goto(S_START);
    }

    void boot()
    {
    }

    void show_startbutton() {
        Buttons.StartButton.gameObject.SetActive(true);
        Buttons.StartButton_pushed = false;
    }

    bool wait_for_startbutton() {
        return Buttons.StartButton_pushed;
    }

    void hide_startbutton() {
        Buttons.StartButton.gameObject.SetActive(false);
    }

    void rocket_show() {
        Globals.parts.rocket.transform.position = Globals.stageMarker.m_rocket_show.transform.position;
        Globals.parts.rocket.SetActive(true);
    }

    // S_MANMOVE
    void mankind_show_and_move(float speed)
    {
        Globals.manMove.gameObject.SetActive(true);
        Globals.manMove.Move(speed);
    }
    bool mankind_move_done()
    {
        return Globals.manMove.IsMoveDone();
    }
    void mankind_hide()
    {
        Globals.manMove.gameObject.SetActive(false);
    }
    
    //S_ZOOM_LAUNCH
    void camera_zoom_at_launch(float speed)
    {
        var cm    = Globals.cameraMove;
        var start = Globals.stageMarker.m_camera_0.transform.position;
        var goal  = Globals.stageMarker.m_camera_1.transform.position;

        cm.Move(start,goal,speed);
    }
    bool camera_zoom_at_launch_done()
    {
        var cm    = Globals.cameraMove;
        return cm.IsMoveDone();
    }

    //S_LAUNCH
    void rocket_launch(float speed)
    {
        var rm = Globals.rocketMove;
        var start = Globals.stageMarker.m_rocket_show.transform.position;
        var goal  = Globals.stageMarker.m_rocket_2.transform.position;
        rm.Move(start,goal,speed);
    }
    //bool rocket_launch_done()
    //{
    //    var rm = Globals.rocketMove;
    //    return rm.IsMoveDone();      
    //}

    //S_LAUNCH_CAM
    void camera_launch(float speed)
    {
        var cm = Globals.cameraMove;
        var start = Globals.stageMarker.m_camera_1.transform.position;
        var goal   = Globals.stageMarker.m_camera_2.transform.position;

        cm.Move(start,goal,speed);
    }
    bool camera_launch_done()
    {
        var cm = Globals.cameraMove;
        return cm.IsMoveDone();
    }


}
