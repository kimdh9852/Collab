using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCreate : MonoBehaviour
{
    //팀 캐릭터들 동기적 소환 스크립트 일시 정지
    public GameObject ChaSlot;
    public GameObject HeroPan;
    public GameObject ChaPan;

    public Canvas cv;

    public Sprite knight;

    public TextAsset jsonData;

    public List<int> MyHero = new List<int>();
    public List<int> MyCha = new List<int>();

    Vector3 isVec = new Vector3(0, 0, 0);

    public Dictionary<int, string> CodeAndName = new Dictionary<int, string>();
    public Dictionary<int, Sprite> sprite = new Dictionary<int, Sprite>();

    public SetAcitveItem sa;
    

    public void Awake()
    {
        LitJson.JsonData getData = LitJson.JsonMapper.ToObject(jsonData.text);
        
        CodeAndName.Add(1001, "Knight");
        CodeAndName.Add(1002, "보르카대검병");
        CodeAndName.Add(1003, "보르카기사");
        CodeAndName.Add(1004, "보르카경비대");
        CodeAndName.Add(1005, "배월교비밀신자");
        CodeAndName.Add(1006, "냉혹한칼잡이");
        CodeAndName.Add(1007, "혹독한활잡이");
        CodeAndName.Add(1008, "청동기계골렘");
        CodeAndName.Add(1009, "임플린열기구병");
        CodeAndName.Add(1010, "왕국병사");
        CodeAndName.Add(1011, "수행중인리자드맨");
        
        sprite.Add(1001, knight);
        sprite.Add(1002, knight);
        sprite.Add(1003, knight);
        sprite.Add(1004, knight);
        sprite.Add(1005, knight);
        sprite.Add(1006, knight);
        sprite.Add(1007, knight);
        sprite.Add(1008, knight);
        sprite.Add(1009, knight);
        sprite.Add(1010, knight);
        sprite.Add(1011, knight);

       HeroPan.GetComponent<RectTransform>().sizeDelta =
            new Vector2(HeroPan.GetComponent<RectTransform>().rect.width + 800,
            HeroPan.GetComponent<RectTransform>().rect.height);

        HeroPan.GetComponent<RectTransform>().position = 
            new Vector2( 0f, HeroPan.GetComponent<RectTransform>().position.y);
        //캐릭터칸 동기적 생성
        for (int i = 0; i < getData["Hero"].Count; i++)
        {
            int code = int.Parse(getData["Hero"][i].ToString());
            MyHero.Add(code);

            GameObject slot = Instantiate(ChaSlot, isVec, Quaternion.identity);
            slot.transform.SetParent(HeroPan.transform);
            slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 250f + 30f, 0f, 0f);
            slot.name = "HeroSlot" + i.ToString();

            slot.transform.Find("Image").name = CodeAndName[code];
            slot.transform.Find(CodeAndName[code]).GetComponent<Image>().sprite = sprite[code];
            slot.GetComponentInChildren<Item>().DefaultImg = sprite[code];
            slot.GetComponentInChildren<Item>().cha = CodeAndName[code];

            sa.ItemList.Add(CodeAndName[code], slot.transform.Find(CodeAndName[code]).gameObject);
        }

        for (int i = 0; i < getData["Cha"].Count; i++)
        {
            int code = int.Parse(getData["Cha"][i].ToString());
            MyCha.Add(code);

            GameObject slot = Instantiate(ChaSlot, isVec, Quaternion.identity);
            slot.transform.SetParent(ChaPan.transform);
            slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 200f + 20f, 0f, 0f);
            slot.name = "ChaSlot" + i.ToString();

            slot.transform.Find("Image").name = CodeAndName[code];
            slot.transform.Find(CodeAndName[code]).GetComponent<Image>().sprite = sprite[code];
            slot.GetComponentInChildren<Item>().DefaultImg = sprite[code];
            slot.GetComponentInChildren<Item>().cha = CodeAndName[code];

            sa.ItemList.Add(CodeAndName[code], slot.transform.Find(CodeAndName[code]).gameObject);          
        }

        
    }
}