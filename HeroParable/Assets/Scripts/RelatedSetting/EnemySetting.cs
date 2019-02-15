using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySetting : MonoBehaviour
{
    public Text Count;
    public Text CounLv;

    public int count = 0;
    public int lv = 1;
    public string mob;
    
    void Awake()
    {
        mob = this.name;
    }

    public void UpArrow()
    {
        if (count == 11)
            return;

        count++;
        Count.text = count.ToString();
    }

    public void DownArrow()
    {
        if (count == 0)
            return;

        count--;
        Count.text = count.ToString();
    }

    public void UpArrowLv()
    {
        if (lv == 51)
            return;

        lv++;
        CounLv.text = lv.ToString();
    }

    public void DownArrowLv()
    {
        if (lv == 1)
            return;

        lv--;
        CounLv.text = lv.ToString();
    }

    
    public void AddMon()
    {
        //잠시 주석처리
        //if (count == 0)
        //    return;
        //GameManager.instance.EnemyUnit.Add(mob);
        //GameManager.instance.EnemyCount.Add(count);
        //GameManager.instance.EnemyLv.Add(lv);

        //stage가 1-1 이면 9마리 lv 1을 소환..
        if (ButtonManager.StageNumber==1.1f)
        {
            GameManager.instance.EnemyUnit.Add("Skelleton_Normal");
            GameManager.instance.EnemyCount.Add(1);
            GameManager.instance.EnemyLv.Add(1);
            GameManager.instance.EnemyUnit.Add("Skelleton_Big");
            GameManager.instance.EnemyCount.Add(1);
            GameManager.instance.EnemyLv.Add(1);
            GameManager.instance.EnemyUnit.Add("Skelleton_Small");
            GameManager.instance.EnemyCount.Add(1);
            GameManager.instance.EnemyLv.Add(1);
        }
        if (ButtonManager.StageNumber == 1.2f)
        {
            GameManager.instance.EnemyUnit.Add("Skelleton_Normal");
            GameManager.instance.EnemyCount.Add(2);
            GameManager.instance.EnemyLv.Add(1);
            GameManager.instance.EnemyUnit.Add("Skelleton_Big");
            GameManager.instance.EnemyCount.Add(2);
            GameManager.instance.EnemyLv.Add(1);
            GameManager.instance.EnemyUnit.Add("Skelleton_Small");
            GameManager.instance.EnemyCount.Add(2);
            GameManager.instance.EnemyLv.Add(1);
        }
        if (ButtonManager.StageNumber == 1.3f)
        {
            GameManager.instance.EnemyUnit.Add("Skelleton_Normal");
            GameManager.instance.EnemyCount.Add(3);
            GameManager.instance.EnemyLv.Add(1);
            GameManager.instance.EnemyUnit.Add("Skelleton_Big");
            GameManager.instance.EnemyCount.Add(3);
            GameManager.instance.EnemyLv.Add(1);
            GameManager.instance.EnemyUnit.Add("Skelleton_Small");
            GameManager.instance.EnemyCount.Add(3);
            GameManager.instance.EnemyLv.Add(1);
        }
    }
}