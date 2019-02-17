using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public enum Type { Human, RizardMan, Elf, Undead, Dragon };
    public enum Elemental { None, Fire, Water, Land, Wood, Dark, Light };

    public Type type;
    public Elemental elemental;

    //공격 및 스킬 확률
    public float normalattack;
    public float skiil1;
    public float skiil2;
    public float skiil3;
    public float skiil4;

    //캐릭터 인포
    public int Lv ;
    public float Hp;
    public float NowHp;
    public float MaxHp;
    public int Power;
    public int Danger;
    public float PhyArmor;
    public float MagicalArmor;
    public float AttackRange;
    public float AttackSpeed;
    public float MoveSpeed;
    public float Critcal;
    public float Evade;
    public float CD;
    public int exp;
    //Hp bar 관련
    public SpriteRenderer sp;
    public Image hpImg;
    //피격 데미지 이펙트
    public GameObject HPParticle;
    //정보값
    public TextAsset jsonData;
    //전투씬의 버튼값 확인
    public string HpBarCheck;

    LitJson.JsonData getData;

    public void Awake()
    {
        getData = LitJson.JsonMapper.ToObject(jsonData.text);

        if (tag == "Player")
        {
            Lv = int.Parse(getData["Level"].ToString());
            GetInfoData(Lv);
            Debug.Log("레벨 " + Lv + "의 " + this.name + "가 소환됨");
        }        
    }

    public void GetInfoData(int lv)
    {
        Lv = lv;

        //skill1,2 추가 
        //skiil1 = float.Parse(getData["Skill1"][Lv - 1].ToString());
        //skiil2 = float.Parse(getData["Skill2"][Lv - 1].ToString());
        skiil1 = 1;
        skiil2 = 2;

        Hp = float.Parse(getData["Hp"][Lv - 1].ToString());
        Power = int.Parse(getData["Power"][Lv - 1].ToString());
        Danger = int.Parse(getData["Danger"].ToString());
        PhyArmor = float.Parse(getData["PhyArmor"][Lv - 1].ToString());
        MagicalArmor = float.Parse(getData["MagicalArmor"][Lv - 1].ToString());
        AttackRange = float.Parse(getData["AttackRange"].ToString());
        AttackSpeed = float.Parse(getData["AttackSpeed"].ToString());
        MoveSpeed = float.Parse(getData["MoveSpeed"].ToString());
        Critcal = float.Parse(getData["Critcal"][Lv - 1].ToString());
        Evade = float.Parse(getData["Evade"][Lv - 1].ToString());
        CD = float.Parse(getData["CD"][Lv - 1].ToString());
        exp = int.Parse(getData["Exp"].ToString());

        MaxHp = NowHp = Hp;
        sp = GetComponentInChildren<SpriteRenderer>();
        hpImg = GetComponentInChildren<Image>();

    }

    public void Update()
    {

        HpBarCheck = GameObject.Find("HpbarCheck").GetComponentInChildren<Text>().text;

        switch (HpBarCheck)
        {
            case "전원 켜기":
                hpImg.gameObject.SetActive(true);
                break;
            case "전원 끄기":
                hpImg.gameObject.SetActive(false);
                break;
            case "아군만":
                if (gameObject.tag == "Player")
                    hpImg.gameObject.SetActive(true);
                else
                    hpImg.gameObject.SetActive(false);
                break;
            case "적군만":
                if (gameObject.tag == "Player")
                    hpImg.gameObject.SetActive(false);
                else
                    hpImg.gameObject.SetActive(true);
                break;
        }
    }

    public void LateUpdate()
    {
        hpImg.fillAmount = (float)Hp / (float)MaxHp;

        if (NowHp != Hp)
        {
            GameObject NewHPP = Instantiate(HPParticle, gameObject.transform.position + new Vector3(0f, 2.5f, 0f), Quaternion.identity) as GameObject;
            NewHPP.GetComponent<AlwaysFace>().Target = GameObject.Find("Main Camera").gameObject;
            TextMesh TM = NewHPP.transform.Find("HPLabel").GetComponent<TextMesh>();

            TM.text = (NowHp - Hp).ToString();

            NewHPP.GetComponent<Rigidbody>().AddForce(
                new Vector3(gameObject.transform.position.x + Random.Range(4f, 10f),
                gameObject.transform.position.y + 30f,
                gameObject.transform.position.z + 5f));

            NowHp = Hp;
            Destroy(NewHPP, 1.0f);
        }
    }
    
}