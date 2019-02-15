using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour
{

    public Transform Img;   // 빈 이미지 객체.

    private Image EmptyImg; // 빈 이미지.
    private Slot slot;      // 현재 슬롯에 스크립트
    public Inventory Iv;
    public Canvas Cv;

    public Inventory check_Iv;

    //int check = 0;

    void Start()
    {
        Cv = GameObject.Find("Canvas").GetComponent<Canvas>();

        // 현재 슬롯의 스크립트를 가져온다.
        slot = GetComponent<Slot>();
        // 빈 이미지 객체를 태그를 이용하여 가져온다.
        Img = GameObject.FindGameObjectWithTag("DragImg").transform;
        // 빈 이미지 객체가 가진 Image컴포넌트를 가져온다.
        EmptyImg = Img.GetComponent<Image>();
        Iv = GameObject.Find("Inventory" + (GameManager.instance.inven_num + 1).ToString()).GetComponent<Inventory>();
        check_Iv = GameObject.Find("Inventory" + 1).GetComponent<Inventory>();
    }
    
    /*private void FixedUpdate()
    {
        if(GameManager.instance.inven_num != check)
        {
            Iv = GameObject.Find("Inventory" + (GameManager.instance.inven_num + 1).ToString()).GetComponent<Inventory>();
            check = GameManager.instance.inven_num;
        }
    }*/


    public void Down()
    {
        // 슬롯에 아이템이 없으면 함수종료.
        if (!slot.isSlots())
            return;


        // 빈 이미지 객체를 활성화 시킨다.
        Img.gameObject.SetActive(true);

        // 빈 이미지의 사이즈를 변경한다.(해상도가 바뀔경우를 대비.)
        float Size = slot.transform.GetComponent<RectTransform>().sizeDelta.x;
        EmptyImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Size);
        EmptyImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Size);

        // 빈 이미지의 스프라이트를 슬롯의 스프라이트로 변경한다.
        EmptyImg.sprite = slot.ItemReturn().DefaultImg;
        // 빈 이미지의 위치를 마우스위로 가져온다.
        Img.transform.position = Input.mousePosition;
        // 슬롯의 아이템 이미지를 없애준다.
        slot.UpdateInfo(true, slot.DefaultImg);
        // 슬롯의 텍스트 숫자를 없애준다.
        //slot.text.text = "";
    }

    public void Drag()
    {
        // isImg플래그가 false이면 슬롯에 아이템이 존재하지 않는 것이므로 함수 종료.
        if (!slot.isSlots())
            return;

        Img.transform.position = Input.mousePosition;
    }

    public void DragEnd()
    {

        // isImg플래그가 false이면 슬롯에 아이템이 존재하지 않는 것이므로 함수 종료.
        if (!slot.isSlots())
            return;
        // 싱글톤을 이용해서 인벤토리의 스왑함수를 호출(현재 슬롯, 빈 이미지의 현재 위치.)

        check_Iv.Swap(slot, Img.transform.position, slot.cha);
        //ObjManager.Call().IV.Swap(slot, Img.transform.position);
        //slot = null;
     
    }

    public void Up()
    {
        // isImg플래그가 false이면 슬롯에 아이템이 존재하지 않는 것이므로 함수 종료.
        if (!slot.isSlots())
            return;

        //마우스가 인벤토리 밖으로 나갈때
        if (Input.mousePosition.x >  Iv.transform.position.x + Iv.InvenWidth  || Input.mousePosition.x < Iv.transform.position.x ||
            Input.mousePosition.y > Iv.transform.position.y || Input.mousePosition.y < Iv.transform.position.y - Iv.InvenHeight)
        {
            Debug.Log(Iv.LastItem_name.IndexOf(slot.cha));
            Debug.Log(slot.cha);
            Iv.LastItem.RemoveAt(Iv.LastItem_name.IndexOf(slot.cha));
            Iv.LastItem_name.Remove(slot.cha);

            Cv.GetComponent<SetAcitveItem>().SetOn(slot.cha);

            slot.isSlot = false;
            slot.cha = null;
            slot.slot.Pop();
            slot.lv = 1;
            slot.UpdateInfo(false, null);
        }
        else  //인벤토리 안에 있을때
        {
            Debug.Log("실패");
            // 슬롯의 아이템 이미지를 복구 시킨다.
            slot.UpdateInfo(true, slot.slot.Peek().DefaultImg);
        }
        // 빈 이미지 객체 비활성화.
        Img.gameObject.SetActive(false);
    }
}
