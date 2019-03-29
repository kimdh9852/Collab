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
    
    // 각 슬롯의 텍스트
    public Text Slot1_text;
    public Text Slot2_text;
    public Text Slot3_text;
    public Text Slot4_text;
    // 각 슬롯의 잠금상태
    static public bool[] Lock = new bool[4];
    // 전체 잠금 확인
    static public bool All_Lock_State = false;

    //프리팹 보관함
    public Dictionary<int, GameObject> AttackUnits = new Dictionary<int, GameObject>();
    public GameObject 검병;
    public GameObject 기마병;
    public GameObject 창병;
    public GameObject 트레뷰셋;
    public GameObject 사제;
    public GameObject 궁병;
    public GameObject 방패병;
    public GameObject 마법사;

    static public List<string> animArray;
    Animation anim;
    static public int index = 0, randomNum = 0;
    static bool Check = false;


    // 테스트용 가격과 텍스트
    static public int[] random_num = new int[7];
    static public string[] random_name = new string[7];
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

        // 테스트용 랜덤 가격
        for (int i = 0; i < 7; i++)
        {
            random_num[i] = Random.Range(0, 7);
        }

        // 잠금상태 확인후 텍스트 변경
        if (!Lock[0])
        {
            Slot1_text.GetComponent<Text>().text = random_name[random_num[0]] + random_num[0].ToString();

            if (Slot1.transform.childCount == 3 )
            {
                Destroy(Slot1.transform.GetChild(2).gameObject);
            }

            GameObject unit_prefab;
            unit_prefab = Instantiate(AttackUnits[random_num[0]]);
            unit_prefab.transform.SetParent(Slot1.transform);

            unit_prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            //unit_prefab.transform.position = new Vector3(0, 0, 0);

            //Slot1.GetComponent<Image>().sprite = img[random_num[0]];
            //Slot1.GetComponent<Animation>().Play(animArray[0]);
            //Slot1.GetComponent<Animation>().wrapMode = WrapMode.Once;
        }
        if (!Lock[1])
        {
            Slot2_text.GetComponent<Text>().text = random_name[random_num[1]] + random_num[1].ToString();

            if(Slot2.transform.childCount == 3)
            {
                Destroy(Slot2.transform.GetChild(2).gameObject);
            }

            GameObject unit_prefab = Instantiate(AttackUnits[random_num[1]]);
            unit_prefab.transform.SetParent(Slot2.transform);

            unit_prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            //unit_prefab.transform.position = new Vector3(0, 0, 0);

            //Slot2.GetComponent<Image>().sprite = img[random_num[1]];
            //Slot2.GetComponent<Animation>().Play(animArray[1]);
            //Slot2.GetComponent<Animation>().wrapMode = WrapMode.Once;
        }
        if (!Lock[2])
        {
            Slot3_text.GetComponent<Text>().text = random_name[random_num[2]] + random_num[2].ToString();

            if (Slot3.transform.childCount == 3)
            {
                Destroy(Slot3.transform.GetChild(2).gameObject);
            }

            GameObject unit_prefab = Instantiate(AttackUnits[random_num[2]]);
            unit_prefab.transform.SetParent(Slot3.transform);

            //unit_prefab.transform.position = new Vector3(0, 0, 0);
            unit_prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            //Slot3.GetComponent<Image>().sprite = img[random_num[2]];
            //Slot3.GetComponent<Animation>().Play(animArray[2]);
            //Slot3.GetComponent<Animation>().wrapMode = WrapMode.Once;
        }
        if (!Lock[3])
        {
            Slot4_text.GetComponent<Text>().text = random_name[random_num[3]] + random_num[3].ToString();

            if (Slot4.transform.childCount == 3)
            {
                Destroy(Slot4.transform.GetChild(2).gameObject);
            }

            GameObject unit_prefab = Instantiate(AttackUnits[random_num[3]]);
            unit_prefab.transform.SetParent(Slot4.transform);

            //unit_prefab.transform.position = new Vector3(0, 0, 0);
            unit_prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

            //Slot4.GetComponent<Image>().sprite = img[random_num[3]];
            //Slot4.GetComponent<Animation>().Play(animArray[3]);
            //Slot4.GetComponent<Animation>().wrapMode = WrapMode.Once;
        }
    }
    // 전체 락/언락
    public void All_Lock()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Lock[i])
            {
                Debug.Log("개인 잠금이 되어있습니다.");
                return;
            }
        }


        if (All_Lock_State)
        {
            All_Lock_State = false;
            for (int i = 0; i < 4; i++)
            {
                Lock[i] = false;
            }
        }
        else
        {
            All_Lock_State = true;
            for (int i = 0; i < 4; i++)
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
                    Lock[3] = false;
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
            RollingAttackUnits unit = GetComponentInChildren<RollingAttackUnits>();
            //인벤토리에 클릭한 유닛을 넣는 구문
            unit.UnitOnClick();

            Lock[num] = false;
            TurnManager.battlegold -= random_num[num];
            Slot.SetActive(false);
            //ui없애는 구문을 여기로
            //인벤토리로 옮기고, 데이터받아와서 데이터에 맞는돈 줄이는 구문도 여기로
        }
    }
    
    private void Start()
    {
        AttackUnits.Add(0, 검병);
        AttackUnits.Add(1, 창병);
        AttackUnits.Add(2, 궁병);
        AttackUnits.Add(3, 방패병);
        AttackUnits.Add(4, 기마병);
        AttackUnits.Add(5, 트레뷰셋);
        AttackUnits.Add(6, 마법사);
        AttackUnits.Add(7, 사제);

        anim = gameObject.GetComponent<Animation>();
        if (!Check)
        {
            animArray = new List<string>();
            AnimationArray();
        }

        for (int i = 0; i < 4; i++)
        {
            Lock[i] = false;
        }

        // 슬롯 설정
        Slot1 = GameObject.Find("sel1");
        Slot2 = GameObject.Find("sel2");
        Slot3 = GameObject.Find("sel3");
        Slot4 = GameObject.Find("sel4");

        // 구문 중복 방지
        if (Slot1_text == null)
            return;

        // 테스트용 초기화 구문
        random_name[0] = "테스트1 : ";
        random_name[1] = "테스트2 : ";
        random_name[2] = "테스트3 : ";
        random_name[3] = "테스트4 : ";
        random_name[4] = "테스트5 : ";
        random_name[5] = "테스트6 : ";
        random_name[6] = "테스트7 : ";

        for (int i = 0; i < 7; i++)
        {
            random_num[i] = i;
        }

        Slot1_text.GetComponent<Text>().text = random_name[random_num[0]] + random_num[0].ToString();
        
        Slot2_text.GetComponent<Text>().text = random_name[random_num[1]] + random_num[1].ToString();   

        Slot3_text.GetComponent<Text>().text = random_name[random_num[2]] + random_num[2].ToString();
       
        Slot4_text.GetComponent<Text>().text = random_name[random_num[3]] + random_num[3].ToString();

        for (int i = 0; i < 4; i++)
        {
            GameObject unit_prefab = Instantiate(AttackUnits[random_num[i]]);
            unit_prefab.transform.SetParent( GameObject.Find("sel" + (i + 1).ToString() ).transform );

            //unit_prefab.transform.position = new Vector3(0, 0, 0);
            unit_prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        }
      
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

    public void AnimationArray()
    {
        Check = true;
        foreach (AnimationState state in anim)
        {
            animArray.Add(state.clip.name);
            index++;
        }
        randomNum = Random.Range(0, index);
    }
}