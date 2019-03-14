using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rolling : MonoBehaviour
{
    //구매시 구매 슬롯을 확인하기위한 슬롯
    public GameObject Slot;
    // 각 슬롯
    static public GameObject Slot1;
    static public GameObject Slot2;
    static public GameObject Slot3;
    static public GameObject Slot4;
    static public GameObject Slot5;
    // 각 슬롯의 텍스트
    public Text Slot1_text;
    public Text Slot2_text;
    public Text Slot3_text;
    public Text Slot4_text;
    public Text Slot5_text;
    // 각 슬롯의 잠금상태
    static public bool Lock1 = false;
    static public bool Lock2 = false;
    static public bool Lock3 = false;
    static public bool Lock4 = false;
    static public bool Lock5 = false;
    // 전체 잠금 확인
    static public bool All_Lock_State = false;

    // 테스트용 가격과 텍스트
    public int[] random_num = new int[5];
    public string[] random_name = new string[5];

    // 리롤
    public void Roll()
    {
        // 구매 후 사라진 슬롯 복구 
        if (!Slot1.activeSelf)
            Slot1.SetActive(true);
        if (!Slot2.activeSelf)
            Slot2.SetActive(true);
        if (!Slot3.activeSelf)
            Slot3.SetActive(true);
        if (!Slot4.activeSelf)
            Slot4.SetActive(true);
        if (!Slot5.activeSelf)
            Slot5.SetActive(true);
        // 테스트용 랜덤 가격
        for (int i = 0; i < 5; i++)
        {
            random_num[i] = Random.Range(0, 4);
        }
        // 잠금상태 확인후 텍스트 변경
        if(!Lock1)
        {
            Slot1_text.GetComponent<Text>().text = random_name[random_num[0]] + random_num[0].ToString();
        }
        if(!Lock2)
        {
            Slot2_text.GetComponent<Text>().text = random_name[random_num[1]] + random_num[1].ToString();
        }
        if(!Lock3)
        {
            Slot3_text.GetComponent<Text>().text = random_name[random_num[2]] +  random_num[2].ToString();
        }
        if(!Lock4)
        {
            Slot4_text.GetComponent<Text>().text = random_name[random_num[3]] +  random_num[3].ToString();
        }
        if(!Lock5)
        {
            Slot5_text.GetComponent<Text>().text = random_name[random_num[4]] +  random_num[4].ToString();
        }
    }

    // 전체 락/언락
    public void All_Lock()
    {
        if (All_Lock_State)
        {
            All_Lock_State = false;
            Lock1 = false;
            Lock2 = false;
            Lock3 = false;
            Lock4 = false;
            Lock5 = false;
        }
        else
        {
            All_Lock_State = true;
            Lock1 = true;
            Lock2 = true;
            Lock3 = true;
            Lock4 = true;
            Lock5 = true;
        }
    }
    // 개인 락/언락
    public void Private_Lock(int num)
    {
        All_Lock_State = false;
        switch (num)
        {
            case 1:
                if (Lock1)
                    Lock1 = false;
                else
                    Lock1 = true;
                break;
            case 2:
                if (Lock2)
                    Lock2 = false;
                else
                    Lock2 = true;
                break;
            case 3:
                if (Lock3)
                    Lock3 = false;
                else
                    Lock3 = true;
                break;
            case 4:
                if (Lock4)
                    Lock4 = false;
                else
                    Lock4 = true;
                break;
            case 5:
                if (Lock5)
                    Lock5 = false;
                else
                    Lock5 = true;
                break;
        }
        //잠금 비용차감 제작
    }

    //마우스를 클릭하면
    public void purchase()
    {
        /*
            if (Player_money < prise)//돈이 없으면
            {
                //돈 부족 텍스트 출력
            }
            else
            {
            ui없애는 구문을 여기로
            인벤토리로 옮기고, 데이터받아와서 데이터에 맞는돈 줄이는 구문도 여기로
            }
        */
        Slot.SetActive(false);
    }


    private void Start()
    {
        // 슬롯 설정
        Slot1 = GameObject.Find("sel1");
        Slot2 = GameObject.Find("sel2");
        Slot3 = GameObject.Find("sel3");
        Slot4 = GameObject.Find("sel4");
        Slot5 = GameObject.Find("sel5");
        // 구문 중복 방지
        if (Slot1_text == null)
            return;

        // 테스트용 초기화 구문
        random_name[0] = "테스트1 : ";
        random_name[1] = "테스트2 : ";
        random_name[2] = "테스트3 : ";
        random_name[3] = "테스트4 : ";
        random_name[4] = "테스트5 : ";

        random_num[0] = 0;
        random_num[1] = 1;
        random_num[2] = 2;
        random_num[3] = 3;
        random_num[4] = 4;


        Slot1_text.GetComponent<Text>().text = random_name[random_num[0]] + random_num[0].ToString();

        Slot2_text.GetComponent<Text>().text = random_name[random_num[1]] + random_num[1].ToString();

        Slot3_text.GetComponent<Text>().text = random_name[random_num[2]] + random_num[2].ToString();

        Slot4_text.GetComponent<Text>().text = random_name[random_num[3]] + random_num[3].ToString();

        Slot5_text.GetComponent<Text>().text = random_name[random_num[4]] + random_num[4].ToString();


    }
    // 잠금 체크 용 디버그
    private void Update()
    {
        if (Lock1)
            Debug.Log("1");
        if (Lock2)
            Debug.Log("2");
        if (Lock3)
            Debug.Log("3");
        if (Lock4)
            Debug.Log("4");
        if (Lock5)
            Debug.Log("5");
    }
}
