using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum TYPE { Hero, Normal }

    public TYPE type;           // 아이템의 타입.
    public Sprite DefaultImg;   // 기본 이미지.
    public int MaxCount;        // 겹칠수 있는 최대 숫자.  
    public RectTransform tr;
    // 인벤토리에 접근하기 위한 변수.
    public Inventory Iv;
    public string cha;
    public int Lv;

    int check = 0;

    public Text this_name;

    void Awake()
    {
        // 태그명이 "Inventory"인 객체의 GameObject를 반환한다.
        // 반환된 객체가 가지고 있는 스크립트를 GetComponent를 통해 가져온다.
        Iv = GameObject.Find("Inventory" + (GameManager.instance.inven_num + 1).ToString()).GetComponent<Inventory>();
        tr = GetComponent<RectTransform>();
        check = 0;
        Lv = 1;
        //cha = gameObject.name;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.inven_num != check)
        {
            Iv = GameObject.Find("Inventory" + (GameManager.instance.inven_num + 1).ToString()).GetComponent<Inventory>();
            check = GameManager.instance.inven_num;
        }
        this_name.text = this.name;
    }

    public void AddItem()
    {
        // 아이템 획득에 실패할 경우.
        if (!Iv.AddItem(this))
            Debug.Log("아이템이 가득 찼습니다.");
        else // 아이템 획득에 성공할 경우.
            Debug.Log("아이템 획득");
            //gameObject.SetActive(false); // 아이템을 비활성화 시켜준다.
    }

    public GameObject beingDragged;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        //if (eventData.position.y >= Iv.gameObject.transform.position.y - Iv.InvenHeight
        //    && eventData.position.y <= Iv.gameObject.transform.position.y
        //    && eventData.position.x >= Iv.gameObject.transform.position.x
        //    && eventData.position.x <= Iv.gameObject.transform.position.x + Iv.InvenWidth)//Iv.InvenRect.rect.xMax)
        {
            Vector2 pos = Input.mousePosition;
            Ray2D ray = new Ray2D(pos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            Inventory check_Iv = GameObject.Find("Inventory" + 1).GetComponent<Inventory>();

            if (hit.collider != null && hit.collider.transform.gameObject.CompareTag("Slot"))
            {
                // 아이템 획득에 실패할 경우.
                if (!check_Iv.testAddItem(this, hit.collider.transform.gameObject))
                {
                    Debug.Log("아이템이 가득 찼습니다.");
                }
                else        // 아이템 획득에 성공할 경우.
                {
                    Debug.Log("아이템 획득.");
                    Iv.LastItem.Add(beingDragged.gameObject);
                    Iv.LastItem_name.Add(beingDragged.name);

                    //드래그가 끝나면 목록에서 안보이게함
                    //beingDragged.gameObject.SetActive(false);

                }
            }
            //빈공간에 놓으면 첫칸으로 가게함
            //else
            //{
            //    Debug.Log("additem");
            //    Iv.LastItem.Add(beingDragged.gameObject);
            //    Iv.LastItem_name.Add(beingDragged.name);

            //    //드래그가 끝나면 목록에서 안보이게함
            //    beingDragged.gameObject.SetActive(false);

            //    AddItem();
            //}
        }

        tr.anchoredPosition = new Vector2(0f,0f);

        beingDragged = null;
    }
}

/*
public class Item : MonoBehaviour
{
    public enum TYPE { HP, MP }

    public TYPE type;           // 아이템의 타입.
    public Sprite DefaultImg;   // 기본 이미지.
    public int MaxCount;        // 겹칠수 있는 최대 숫자.  

    // 인벤토리에 접근하기 위한 변수.
    private Inventory Iv;

    void Awake()
    {
        // 태그명이 "Inventory"인 객체의 GameObject를 반환한다.
        // 반환된 객체가 가지고 있는 스크립트를 GetComponent를 통해 가져온다.
        Iv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

    }

    public void AddItem()
    {
        // 아이템 획득에 실패할 경우.
        if (!Iv.AddItem(this))
            Debug.Log("아이템이 가득 찼습니다.");
        else // 아이템 획득에 성공할 경우.
            gameObject.SetActive(false); // 아이템을 비활성화 시켜준다.
    }

}*/