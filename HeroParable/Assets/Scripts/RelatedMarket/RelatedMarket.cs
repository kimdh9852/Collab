using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelatedMarket : MonoBehaviour {

    public static int Money;
    public static int Cash;

    public GameObject SuccessBuy;
    public GameObject FailBuy;

    public Text Money_Text;
    public Text Cash_Text;

    public TextAsset jsonData;

    int common = 0, uncommon = 0, rare = 0, uniq = 0, legend = 0, total = 0; //, total_legend = 0;

    public void Awake()
    {
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);

        Money = int.Parse(getData["Money"].ToString());
        Cash = int.Parse(getData["Cash"].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Money_Text.text =  Money.ToString();
        Cash_Text.text = Cash.ToString();
    }

    public void BuyingMoney(int item_Money)
    {
        if (Money >= item_Money)
        {
            Money -= item_Money;
            SuccessBuy.gameObject.SetActive(true);
        }
        else
            FailBuy.gameObject.SetActive(true);
    }
    public void BuyingCash(int item_Cash)
    {
        if (Cash >= item_Cash)
        {
            Cash -= item_Cash;
            SuccessBuy.gameObject.SetActive(true);
        }
        else
            FailBuy.gameObject.SetActive(true);
    }

    public void Confirm()
    {
        SuccessBuy.SetActive(false);
        FailBuy.SetActive(false);
    }

    public void RandomBox(int item_Money)
    {
        int random;

        if (Money < item_Money)
        {
            FailBuy.SetActive(true);
            return;
        }

        Money -= item_Money;

        common = 0;
        uncommon = 0;
        rare = 0;
        uniq = 0;
        legend = 0;

        for (int i = 0; i < 10; i++)
        {
            random = Random.Range(1, 10000);

            SuccessBuy.gameObject.SetActive(true);
            if (random < 5001)//5000
            {
                common++;
                total++;
            }
            else if (random < 8001)//3000
            {
                uncommon++; 
                total++;
            }
            else if (random < 9501)//1500
            {
                rare++; 
                total++;
            }
            else if (random < 9999)//499
            {
                uniq++; 
                total++;
            }
            else //1
            {
                legend++;
                total++;
            }
        }

        SuccessBuy.gameObject.transform.GetChild(0).GetComponent<Text>().text = "커먼템 x " + common.ToString() + "획득";
        SuccessBuy.gameObject.transform.GetChild(1).GetComponent<Text>().text = "언커먼템 x " + uncommon.ToString() + "획득";
        SuccessBuy.gameObject.transform.GetChild(2).GetComponent<Text>().text = "레어템 x " + rare.ToString() + "획득";
        SuccessBuy.gameObject.transform.GetChild(3).GetComponent<Text>().text = "유니크템 x " + uniq.ToString() + "획득";
        SuccessBuy.gameObject.transform.GetChild(4).GetComponent<Text>().text = "레전드템 x " + legend.ToString() + "획득";

        //Debug.Log(" common : " + common + " uncommon : " + uncommon + " rare : "
        //           + rare + " uniq : " + uniq + " legend : " + legend + " Total : " + total + " total_legend : " + total_legend);

    }
    //public Rect windowSize = new Rect(15, 15, 250, 250);

    //private void OnGUI()
    //{
    //    windowSize = GUI.Window(1000, windowSize, DoMyWindow, "My Window");
    //}

    //void DoMyWindow(int windowID)
    //{
    //    GUI.Button(new Rect(windowSize.width * 0.25f, windowSize.height * 0.6f, 50, 50), "구매");
    //    GUI.Button(new Rect(windowSize.width * 0.65f, windowSize.height * 0.6f, 50, 50), "취소");

    //    GUI.DragWindow();
    //}
}
