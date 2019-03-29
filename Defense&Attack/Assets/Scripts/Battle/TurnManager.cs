using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Rolling rolling;
    public Text goldText;
    public Text TimeText;
    public Text roundText;
    public Text stateText;
    public bool battlestart = false;
    public float second;
    public float prepared = 10f;
    public float ready = 5f;
    public float battletime = 10f;
    //흑
    public bool user1turn = true;
    //백
    public bool user2turn = false;
    
    //스폰 한번만 이뤄지도록
    public bool spawn = true;

    static public int round;
    static public int battlegold;

    public void Awake()
    {
        battlegold = 1000;
        second = 5.0f;
        prepared = 10f;
        ready = 5f;
        battletime = 10f;
        round = 1;
        user1turn = true;
        user2turn = false;
    }

    public void Update()
    {
        goldText.text = battlegold.ToString();
        roundText.text = "ROUND : " + round.ToString();
        if(battlestart==false)
        {
            stateText.text = "Wait";
            second -= Time.deltaTime;
            TimeText.text = ((int)second).ToString();
        }
        if(second <0)
        {
            battlestart = true;
            stateText.text = "Prepared";
            prepared -= Time.deltaTime;
            TimeText.text = ((int)prepared).ToString();
        }
        if(prepared < 0)
        {
            second = 0;
            stateText.text = "Ready";
            ready -= Time.deltaTime;
            TimeText.text = ((int)ready).ToString();
        }
        if (ready < 0)
        {
            prepared = 10f;
            stateText.text = "Battle";
            battletime -= Time.deltaTime;
            TimeText.text = ((int)battletime).ToString();

            if (spawn)
            {
                GameObject.Find("User1Spawn").GetComponent<User1WaveSpawn>().SettingCheck();
                spawn = false;
            }
        }
        if(battletime < 0)
        {
            rolling.Roll(false);
            ready = 5f;
            round++;
            battletime = 10f;
            second = -1;
            TurnChange();
            battlegold += round;
        }
    }

    public void TurnChange()
    {
        if (user1turn)
        {
            user1turn = false;
            user2turn = true;
        }
        else
        {
            user1turn = true;
            user2turn = false;
        }
    }
}
