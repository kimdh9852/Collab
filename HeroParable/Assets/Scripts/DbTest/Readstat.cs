using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Readstat : MonoBehaviour
{
    public Text Hp;
    public int hp;
    public Text AP;
    public int ap;
    public Text AR;
    public float ar;
    public Text MS;
    public float ms;
    public Text Sp;

    public int Statpoint = 5;

    public string[] stats;

    //스탯을 db에서 불러오는 코드
    IEnumerator Start()
    {
        //db주소 받아오는 코드
        WWW ReadStatData = new WWW("http://localhost/heroparable/readstat.php");
        //받아올때까지 대기
        yield return ReadStatData;

        string RSDString = ReadStatData.text;
        //받아온 정보들은 한줄의 긴 문장이기때문에|를 기준으로 분리하여 배열에 저장
        stats = RSDString.Split('|');

        for (int i = 0; i < stats.Length; i++)
        {
            //정보의 끝값에는 ;가 붙어있기때문에 이를 제거하는 작업을 거침
            stats[i] = GetDataValue(stats[i]);
        }
        //string정보들을 각 스탯 형식에 맞도록 파싱
        Parshing_Stats();
    }

    public void Parshing_Stats()
    {
        hp = int.Parse(stats[0]);
        ap = int.Parse(stats[1]);
        ar = float.Parse(stats[2]);
        ms = float.Parse(stats[3]);       
    }
    
    string GetDataValue(string data)
    {
        string value = data;

        if (value.Contains(";"))
            value = value.Remove(value.IndexOf(";"));
        
        return value;
    }
    //스탯을 디비에 업데이트하는 코드
    public void Statconfirm()
    {
        //php파일에는 변수들을 POST하기위하여 form에 값들을 저장
        //float값을 전달 안됨 ->ToString()으로 해결됨을 확인
        WWWForm form = new WWWForm();
        form.AddField("Hp", hp);
        form.AddField("AttackPower", ap);
        form.AddField("AttackRange", ar.ToString());
        form.AddField("MoveSpeed", ms.ToString());
        //디비 주소에다가 POST값을 전달. 수정은 php파일 내에서 이뤄짐
        WWW www = new WWW("http://localhost/heroparable/updatestat.php", form);
    }

    public void StatUp(string stat)
    {
        if (Statpoint == 0)
            return;

        Statpoint--;

        switch (stat)
        {
            case "Hp":
                hp += 100;
                break;
            case "Ap":
                ap += 100;
                break;
            case "Ar":
                ar += 0.5f;
                break;
            case "Ms":
                ms += 0.5f;
                break;
        }
    }

    public void StatDown(string stat)
    {
        if (Statpoint == 5 )
            return;

        Statpoint++;

        switch (stat)
        {
            case "Hp":
                hp -= 100;
                break;
            case "Ap":
                ap -= 100;
                break;
            case "Ar":
                ar -= 0.5f;
                break;
            case "Ms":
                ms -= 0.5f;
                break;
        }
    }

    private void Update()
    {
        Hp.text = hp.ToString();
        AP.text = ap.ToString();
        AR.text = ar.ToString();
        MS.text = ms.ToString();
        Sp.text = Statpoint.ToString();
    }

}