using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSetting : MonoBehaviour {
    
    public void SettingEnd()
    {
        GameManager.instance.SettingCheck();

        if (GameManager.instance.chaList[0].Count == 0 && GameManager.instance.chaList[1].Count == 0)
            return;

        for (int i = 0; i < 1; i++)
        {
            //GameObject.Find("EnemyListPan").GetComponentInChildren<EnemySetting>().AddMon();
            GameObject.Find("EnemyListPan").transform.GetChild(i).GetComponent<EnemySetting>().AddMon();
        }


        GameObject.Find("SceneMover").GetComponent<SceneMove>().GoBattle();
    }
}