using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Text TimeText;
    public float sec;

    public bool user1turn = true;
    public bool user2turn = false;
    public int round;

    public void Awake()
    {
        sec = 10.0f;
        round = 1;
        TimeChek();
    }

    public void Update()
    {
        TimeText.text = ((int)sec).ToString();
    }

    public void TimeChek()
    {
        InvokeRepeating("TimeCoroutine", 10f, Time.deltaTime); ;
    }

    public void TimeCoroutine()
    {
        if (sec <= 0.0f)
        {
            return;
        }

        SecondChek();
    }

    void SecondChek()
    {
        sec -= Time.deltaTime;
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
