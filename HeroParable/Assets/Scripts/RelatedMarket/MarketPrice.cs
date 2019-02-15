using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPrice : MonoBehaviour
{

    public int item1_Money;
    public int item1_Cash;
    public int item2_Money;
    public int item2_Cash;

    public Text item1_Money_Text;
    public Text item1_Cash_Text;
    public Text item2_Money_Text;
    public Text item2_Cash_Text;

    public TextAsset jsonData;

    public void Awake()
    {
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);
        item1_Money = int.Parse(getData["item1_Money"].ToString());
        item1_Cash = int.Parse(getData["item1_Cash"].ToString());
        item2_Money = int.Parse(getData["item2_Money"].ToString());
        item2_Cash = int.Parse(getData["item2_Cash"].ToString());
    }

    void Update()
    {
        item1_Money_Text.text = "Money = " + item1_Money.ToString();
        item1_Cash_Text.text = "Cash = " + item1_Cash.ToString();
        item2_Money_Text.text = "Money = " + item2_Money.ToString();
        item2_Cash_Text.text = "Cash = " + item2_Cash.ToString();
    }
}
