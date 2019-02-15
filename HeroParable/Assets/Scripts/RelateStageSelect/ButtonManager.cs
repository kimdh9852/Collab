using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

   public SceneMove SceneMove;
    public static float StageNumber = 0;

    public void Stage1_1()
    {
        //StageNumber = 1.3f;
        StageNumber = 1.1f;
        SceneMove.GoSetting();
    }

    public void Stage1_2()
    {
        StageNumber = 1.2f;
        SceneMove.GoSetting();
    }

    public void Stage1_3()
    {

        StageNumber = 1.3f;
        SceneMove.GoSetting();
    }
}
