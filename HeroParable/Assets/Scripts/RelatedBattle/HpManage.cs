using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManage : MonoBehaviour {

    public Image totalImg;

    public float nowTotalHp;
    public float maxTotalHp;

    GameObject[] liili;

    float count;
   
    public void Start()
    {
        liili = GameObject.FindGameObjectsWithTag(gameObject.name);

        for (int i = 0; i < liili.Length; i++)
        {
            nowTotalHp += liili[i].GetComponent<Info>().Hp;
        }

        maxTotalHp = nowTotalHp;
    }

    public void Update()
    {
        for (int i = 0; i < liili.Length; i++)
        {
            if (liili[i].GetComponent<Info>().Hp >= 0)
                count += liili[i].GetComponent<Info>().Hp;
            else
                count += 0;
        }
        nowTotalHp = count;

        count = 0;

        totalImg.fillAmount = (float)nowTotalHp / (float)maxTotalHp;
    }
}
