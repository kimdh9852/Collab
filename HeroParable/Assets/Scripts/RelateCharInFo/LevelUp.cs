using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public Text Exp;
    public int exp;
    public Text Ac_Exp;
    public int ac_exp;
    public Text Lv;
    public int lv = 1;
    public Text Re_Exp;
    public int required_exp;

    public GameObject Failed_LvUp;


    public void Start()
    {
        required_exp = 100 + (lv * 10);
    }
    public void Update()
    {
        Exp.text = exp.ToString();
        Lv.text = lv.ToString();
        Ac_Exp.text = ac_exp.ToString();
        Re_Exp.text = required_exp.ToString();
        required_exp = 100 + lv * 10;
    }

    float over100 = 0.99309f;

    public void Normal_LeveUp()
    {
        if (exp < required_exp)
        {
            Failed_LvUp.SetActive(true);
            return;
        }

        if (lv < 100)
        {
            exp -= required_exp;
            lv++;
            
            return;
        }

        float random = Random.Range(0.00000f, 100f);
        float UpPercent = 100f * Mathf.Pow(over100,(lv - 100));

        Debug.Log("난수 발생:" + random + "/////// 업할 확률: " + UpPercent);

        if ( UpPercent >= random )
        {
            exp -= required_exp;
            lv++;
            Debug.Log("레벨 업 성공!");
        }
        else
        {
            exp -= required_exp;
            Debug.Log("레벨 업 실패!  경험치만 날렸군요 휴먼");
        }
    }

    public void GetExp(int get_exp)
    {
        exp += get_exp;
        ac_exp += get_exp;
    }

    public void ClosePanel()
    {
        Failed_LvUp.SetActive(false);
    }

}
