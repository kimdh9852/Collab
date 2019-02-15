using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCheck : MonoBehaviour {

    public Text timer;

    public int minute;
    public int second;
    public static float second_13;

    void Awake () {

        timer = GetComponent<Text>();
        minute = 0;
        second = 0;
        second_13 = 16f;
	}
	
	void Update () {
        
        second = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        if (second == 60)
        {
            minute++;
            second = 0;
        }
        //1_3stage 전용 타이머
        if(ButtonManager.StageNumber ==1.3f)
        {
            second_13 -= Time.deltaTime;
            timer.text = "Time : " + minute + ":" + (int)second_13;
        }
        else
        {
            timer.text = "Time : " + minute + ":" + second;
        }
	}
}
