using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBtnManager : MonoBehaviour
{
    static public bool Btn_chang = false;
    // 조합 창
    public GameObject chang;
    // 병사 창
    public GameObject humanchang;
    // 롤링 창
    public GameObject RollingChang;

    //각각의 버튼이 눌렸는지 확인함
    static public bool Hbtn_1 = false;
    static public bool Hbtn_2 = false;
    static public bool Hbtn_3 = false;
    static public bool Hbtn_4 = false;
    static public bool Hbtn_5 = false;
    static public bool Hbtn_6 = false;
    static public bool Hbtn_7 = false;
    static public bool Hbtn_8 = false;

    //숫자 체크
    static public int AllH = 0;
    static public int H1 = 0;
    static public int H2 = 0;
    static public int H3 = 0;
    static public int H4 = 0;
    static public int H5 = 0;
    static public int H6 = 0;
    static public int H7 = 0;
    static public int H8 = 0;

    //롤링 창 체크
    static public bool Rolling_state = false;

    // 원소 칸 클릭
    public void OnClickElement()
    {
        humanchang.SetActive(false);
    }
    // 인간 칸 클릭
    public void OnClickHuman()
    {
        humanchang.SetActive(true);
    }
    // 롤링 버튼 클릭
    public void OnClickRoll()
    {
        if (Rolling_state)
        {
            RollingChang.SetActive(false);
            Rolling_state = false;
        }
        else
        {
            RollingChang.SetActive(true);
            Rolling_state = true;
        }
    }
    // 원소 조합창 나가기 클릭
    public void OnClickEExit()
    {
        chang.SetActive(false);
    }
    //원소창 버튼 "1" 클릭
    public void OnClick1()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }
    //원소창 버튼 "2" 클릭
    public void OnClickE2()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }
    //원소창 버튼 "3" 클릭
    public void OnClickE3()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }
    //원소창 버튼 "4" 클릭
    public void OnClickE4()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }
    //원소창 버튼 "5" 클릭
    public void OnClickE5()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }
    //원소창 버튼 "6" 클릭
    public void OnClickE6()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }
    //원소창 버튼 "7" 클릭
    public void OnClickE7()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }
    //원소창 버튼 "8" 클릭
    public void OnClickE8()
    {
        Btn_chang = true;
        chang.SetActive(true);
    }

    //인간창 버튼 "1" 클릭
    public void OnClickH1()
    {
        if(Hbtn_1 == false)
        {
            Hbtn_1 = true;
            H1 = 1;
            Debug.Log("FH1 : " + H1);
        }
        else
        {
            Hbtn_1 = false;
            H1 = 0;
            Debug.Log("TH1 : " + H1);
        }
    }
    //인간창 버튼 "2" 클릭
    public void OnClickH2()
    {
        if (Hbtn_2 == false)
        {
            Hbtn_2 = true;
            H2 = 1;
            Debug.Log(H2);
        }
        else if (Hbtn_2 == true)
        {
            Hbtn_2 = false;
            H2 = 0;
            Debug.Log(H2);
        }
    }
    //인간창 버튼 "3" 클릭
    public void OnClickH3()
    {
        if (Hbtn_3 == false)
        {
            Hbtn_3 = true;
            H3 = 1;
            Debug.Log(H3);
        }
        else if (Hbtn_3 == true)
        {
            Hbtn_3 = false;
            H3 = 0;
            Debug.Log(H3);
        }
    }
    //인간창 버튼 "4" 클릭
    public void OnClickH4()
    {
        if (Hbtn_4 == false)
        {
            Hbtn_4 = true;
            H4 = 1;
            Debug.Log(H4);
        }
        else if (Hbtn_4 == true)
        {
            Hbtn_4 = false;
            H4 = 0;
            Debug.Log(H4);
        }
    }
    //인간창 버튼 "5" 클릭
    public void OnClickH5()
    {
        if (Hbtn_5 == false)
        {
            Hbtn_5 = true;
            H5 = 1;
            Debug.Log(H5);
        }
        else if (Hbtn_5 == true)
        {
            Hbtn_5 = false;
            H5 = 0;
            Debug.Log(H5);
        }
    }
    //인간창 버튼 "6" 클릭
    public void OnClickH6()
    {
        if (Hbtn_6 == false)
        {
            Hbtn_6 = true;
            H6 = 1;
            Debug.Log(H6);
        }
        else if (Hbtn_6 == true)
        {
            Hbtn_6 = false;
            H6 = 0;
            Debug.Log(H6);
        }
    }
    //인간창 버튼 "7" 클릭
    public void OnClickH7()
    {
        if (Hbtn_7 == false)
        {
            Hbtn_7 = true;
            H7 = 1;
            Debug.Log(H7);
        }
        else if (Hbtn_7 == true)
        {
            Hbtn_7 = false;
            H7 = 0;
            Debug.Log(H7);
        }
    }
    //인간창 버튼 "8" 클릭
    public void OnClickH8()
    {
        if (Hbtn_8 == false)
        {
            Hbtn_8 = true;
            H8 = 1;
            Debug.Log(H8);
        }
        else if (Hbtn_8 == true)
        {
            Hbtn_8 = false;
            H8 = 0;
            Debug.Log(H8);
        }
    }

    //인간창 버튼 "확인" 클릭
    public void OnClickHOK()
    {
        AllH = H1 + H2 + H3 + H4 + H5 + H6 + H7 + H8;
        Debug.Log("AllH의 값 : " + AllH);
        Hbtn_1 = false;
        Hbtn_2 = false;
        Hbtn_3 = false;
        Hbtn_4 = false;
        Hbtn_5 = false;
        Hbtn_6 = false;
        Hbtn_7 = false;
        Hbtn_8 = false;
        humanchang.SetActive(false);
    }

    //인간창 버튼 "취소" 클릭
    public void OnClickHCancle()
    {
        H1 = 0;
        H2 = 0;
        H3 = 0;
        H4 = 0;
        H5 = 0;
        H6 = 0;
        H7 = 0;
        H8 = 0;
        Hbtn_1 = false;
        Hbtn_2 = false;
        Hbtn_3 = false;
        Hbtn_4 = false;
        Hbtn_5 = false;
        Hbtn_6 = false;
        Hbtn_7 = false;
        Hbtn_8 = false;
        humanchang.SetActive(false);
    }

    void Update()
    {
        //Btn_chang -> Tile에서 산 땅에 건물을 지으면 창이 사라짐 Tile.cs 34

        if (Btn_chang == false && chang != null)
        {
            chang.SetActive(false);
        }
    }
}
