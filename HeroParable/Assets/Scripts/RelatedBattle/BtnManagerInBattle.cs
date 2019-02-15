using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManagerInBattle : MonoBehaviour{

    public GameObject optionPan;

    public string HpState = "All";
    public Text HpbarT;

    public void OpPanON()
    {
        optionPan.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitopPan()
    {
        optionPan.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
    // 배틀씬에서 액티브 스킬을 사용하기위한 토글 함수
    public void ActiveSkillButton()
    {
        Ai.buttoncheck = 1;
        Debug.Log("버튼눌림");
    }

    public void ChangeHPbar()
    {
        switch (HpState)
        {
            case "All":
                HpState = "Close";
                HpbarT.text = "전원 끄기";
                break;
            case "Close":
                HpState = "Team";
                HpbarT.text = "아군만";
                break;
            case "Team":
                HpState = "Enemy";
                HpbarT.text = "적군만";
                break;
            case "Enemy":
                HpState = "All";
                HpbarT.text = "전원 켜기";
                break;
        }
    }

   /* public void test()
    {
        if(Time.timeScale == 3)
        {
            Time.timeScale = 0.5f;
            return;
        }
        if (Time.timeScale == 0.5f)
            Time.timeScale = 3;

        switch ((int)Time.timeScale)
        {
            case 3:
                Time.timeScale = 1;
                break;
            case 1:
                Time.timeScale = 2;
                break;
            case 2:
                Time.timeScale = 3;
                break;
            default:
                break;
        }
    }*/
}