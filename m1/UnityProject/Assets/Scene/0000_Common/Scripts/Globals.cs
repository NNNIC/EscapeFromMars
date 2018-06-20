using UnityEngine;
using System.Collections;

public class Globals {

    //public const float deltaTime = 1.0f / 30.0f;   //３０フレーム固定

    public const float referenceScreenWidth = 640;
    public const float referenceScreenHeight= 1136;

    public static readonly string staticGameObjectName = "StaticObject";
    public static GameObject staticObject;

    public static Parts       parts;
    public static StageMarker stageMarker;

    public static ManMove     manMove;
    public static RocketMove  rocketMove;
    //public static CameraMove  cameraMove;
}
