using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Text TimeText;
    public float second;

    //흑
    public bool user1turn = true;
    //백
    public bool user2turn = false;
    public int round;

    public void Awake()
    {
        second = 10.0f;
        round = 1;
        user1turn = true;
        user2turn = false;
    }

    public void Update()
    {
        if (TimeText.text == "Wait")
            return;
        
        TimeText.text = ((int)second).ToString();
    }

    public void TimeChek()
    {
        
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
