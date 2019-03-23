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
    static public bool[] Lock = new bool[5];
    // 전체 잠금 확인
    static public bool All_Lock_State = false;

    // 테스트용 가격과 텍스트
    static public int[] random_num = new int[5];
    static public string[] random_name = new string[5];
    // 개인 잠금한 갯수
    static public int PrivateLockCount = 0;
    // 리롤
    public void Roll(bool UseMoney)
    {
        if (!RollingMoney(UseMoney))
            return;

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
        if (!Lock[0])
        {
            Slot1_text.GetComponent<Text>().text = random_name[random_num[0]] + random_num[0].ToString();
        }
        if (!Lock[1])
        {
            Slot2_text.GetComponent<Text>().text = random_name[random_num[1]] + random_num[1].ToString();
        }
        if (!Lock[2])
        {
            Slot3_text.GetComponent<Text>().text = random_name[random_num[2]] + random_num[2].ToString();
        }
        if (!Lock[3])
        {
            Slot4_text.GetComponent<Text>().text = random_name[random_num[3]] + random_num[3].ToString();
        }
        if (!Lock[4])
        {
            Slot5_text.GetComponent<Text>().text = random_name[random_num[4]] + random_num[4].ToString();
        }
    }

    // 전체 락/언락
    public void All_Lock()
    {
        if (All_Lock_State)
        {
            All_Lock_State = false;
            for (int i = 0; i < 5; i++)
            {
                Lock[i] = false;
            }
        }
        else
        {
            All_Lock_State = true;
            for (int i = 0; i < 5; i++)
            {
                Lock[i] = true;
            }
        }
    }
    // 개인 락/언락
    public void Private_Lock(int num)
    {
        if (All_Lock_State)
        {
            Debug.Log("전체 잠금을 해제해주세요!");
            return;
        }
        switch (num)
        {
            case 1:       
                if (!Lock[0])
                {
                    if (TurnManager.battlegold < PrivateLockCount + 1)
                    {
                        Debug.Log("잠금 불가");
                        return;
                    }
                    PrivateLockCount++;
                    TurnManager.battlegold -= PrivateLockCount;
                    Lock[0] = true;
                }
                else
                {
                    PrivateLockCount--;
                    Lock[0] = false;
                }
                break;
            case 2:
                if (!Lock[1])
                {
                    if (TurnManager.battlegold < PrivateLockCount + 1)
                    {
                        Debug.Log("잠금 불가");
                        return;
                    }
                    PrivateLockCount++;
                    TurnManager.battlegold -= PrivateLockCount;
                    Lock[1] = true;
                }
                else
                {
                    PrivateLockCount--;
                    Lock[1] = false;
                }
                break;
            case 3:
                if (!Lock[2])
                {
                    if (TurnManager.battlegold < PrivateLockCount + 1)
                    {
                        Debug.Log("잠금 불가");
                        return;
                    }
                    PrivateLockCount++;
                    TurnManager.battlegold -= PrivateLockCount;
                    Lock[2] = true;
                }
                else
                {
                    PrivateLockCount--;
                    Lock[2] = false;
                }
                break;
            case 4:
                if (!Lock[3])
                {
                    if (TurnManager.battlegold < PrivateLockCount + 1)
                    {
                        Debug.Log("잠금 불가");
                        return;
                    }
                    PrivateLockCount++;
                    TurnManager.battlegold -= PrivateLockCount;
                    Lock[3] = true;
                }
                else
                {
                    PrivateLockCount--;
                    Lock[3] = false;
                }
                break;
            case 5:
                if (!Lock[4])
                {
                    if (TurnManager.battlegold < PrivateLockCount + 1)
                    {
                        Debug.Log("잠금 불가");
                        return;
                    }
                    PrivateLockCount++;
                    TurnManager.battlegold -= PrivateLockCount;
                    Lock[4] = true;
                }
                else
                {
                    PrivateLockCount--;
                    Lock[4] = false;
                }
                break;
        }
    }

    //마우스를 클릭하면
    public void purchase(int num)
    {

        if (TurnManager.battlegold < random_num[num])//돈이 없으면
        {
            Debug.Log("어림도 없다");
        }
        else
        {
            TurnManager.battlegold -= random_num[num];
            Slot.SetActive(false);
            //ui없애는 구문을 여기로
            //인벤토리로 옮기고, 데이터받아와서 데이터에 맞는돈 줄이는 구문도 여기로
        }

    }


    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Lock[i] = false;
        }

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
        if (Lock[0])
            Debug.Log("1");
        if (Lock[1])
            Debug.Log("2");
        if (Lock[2])
            Debug.Log("3");
        if (Lock[3])
            Debug.Log("4");
        if (Lock[4])
            Debug.Log("5");
    }
    public bool RollingMoney(bool UseMoney)
    {
        if (!UseMoney)
            return true;

        if (TurnManager.round < 11)
        {
            if (TurnManager.battlegold < 1)
            {
                Debug.Log("돈부족");
                return false;
            }
            TurnManager.battlegold -= 1;
            return true;
        }
        else if (TurnManager.round < 21)
        {
            if (TurnManager.battlegold < 2)
            {
                Debug.Log("돈부족");
                return false;
            }
            TurnManager.battlegold -= 2;
            return true;
        }
        else if (TurnManager.round < 31)
        {
            if (TurnManager.battlegold < 3)
            {
                Debug.Log("돈부족");
                return false;
            }
            TurnManager.battlegold -= 3;
            return true;
        }
        else if (TurnManager.round < 41)
        {
            if (TurnManager.battlegold < 4)
            {
                Debug.Log("돈부족");
                return false;
            }
            TurnManager.battlegold -= 4;
            return true;
        }
        else
        {
            if (TurnManager.battlegold < 5)
            {
                Debug.Log("돈부족");
                return false;
            }
            TurnManager.battlegold -= 5;
            return true;
        }
    }
}