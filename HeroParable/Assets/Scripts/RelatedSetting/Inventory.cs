﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // 공개
    public List<GameObject> AllSlot;    // 모든 슬롯을 관리해줄 리스트.
    public RectTransform InvenRect;     // 인벤토리의 Rect
    public GameObject OriginSlot;       // 오리지널 슬롯.

    public float slotSize;              // 슬롯의 사이즈.
    public float slotGap;               // 슬롯간 간격.
    public float slotCountX;            // 슬롯의 가로 개수.
    public float slotCountY;            // 슬롯의 세로 개수.

    // 비공개.
    public float InvenWidth;           // 인벤토리 가로길이.
    public float InvenHeight;          // 인밴토리 세로길이.
                                       //private float EmptySlot;          // 빈 슬롯의 개수.

    //test용 list
    public List<GameObject> LastItem;
    public List<string> LastItem_name;

    static bool check = true;
    static int check_num = 0;

    void Awake()
    {
        // 인벤토리 이미지의 가로, 세로 사이즈 셋팅.
        InvenWidth = (slotCountX * slotSize) + (slotCountX * slotGap) + slotGap;
        InvenHeight = (slotCountY * slotSize) + (slotCountY * slotGap) + slotGap;

        // 셋팅된 사이즈로 크기를 설정.
        InvenRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, InvenWidth); // 가로.
        InvenRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InvenHeight);  // 세로.

        if (!check)
        {
            if (check_num < 1)
            {
                check_num++;
                return;
            }
            check_num = 0;
            check = true;
        }
        // 슬롯 생성하기.
        for (int i = 0; i < 2; i++)
        {
            check = false;
            Transform _parent = GameObject.Find("Inventory" + (i+1).ToString()).transform;

            for (int y = 0; y < slotCountY; y++)
            {
                for (int x = 0; x < slotCountX; x++)
                {
                    // 슬롯을 복사한다.
                    GameObject slot = Instantiate(OriginSlot) as GameObject;
                    // 슬롯의 RectTransform을 가져온다.
                    RectTransform slotRect = slot.GetComponent<RectTransform>();
                    // 슬롯의 자식인 투명이미지의 RectTransform을 가져온다.
                    RectTransform item = slot.transform.GetChild(0).GetComponent<RectTransform>();

                    slot.name = "slot_" + y + "_" + x + "_invnum : " + i;   // 슬롯 이름 설정.
                    slot.transform.SetParent(_parent); // 슬롯의 부모를 설정. (Inventory객체가 부모임.)

                    slot.GetComponent<Slot>().xloc = x - 10 * i;
                    slot.GetComponent<Slot>().yloc = y;

                    // 슬롯이 생성될 위치 설정하기.
                    slotRect.localPosition = new Vector3((slotSize * x) + (slotGap * (x + 1)),
                                                       -((slotSize * y) + (slotGap * (y + 1))),
                                                          0);

                    // 슬롯의 자식인 투명이미지의 사이즈 설정하기.
                    slotRect.localScale = Vector3.one;
                    slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize); // 가로
                    slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);   // 세로.

                    // 슬롯의 사이즈 설정하기.
                    item.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize - slotSize * 0.3f); // 가로.
                    item.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize - slotSize * 0.3f);   // 세로.

                    // 리스트에 슬롯을 추가.
                    AllSlot.Add(slot);

                    if (GameManager.instance.AllSlot.Count < 2)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            GameManager.instance.AllSlot.Add(new List<GameObject>());
                        }
                    }
                    GameManager.instance.AllSlot[i].Add(slot);
                }
            }
        }
        //Transform inventory2 = GameObject.Find("Inventory2").transform;

        //inventory2.gameObject.SetActive(false);

        // 빈 슬롯 = 슬롯의 숫자.
        //EmptySlot = AllSlot.Count;
    }    

    // 아이템을 넣기위해 모든 슬롯을 검사.
    public bool AddItem(Item item)
    {
        // 슬롯에 총 개수.
        int slotCount = AllSlot.Count;

        // 넣기위한 아이템이 슬롯에 존재하는지 검사.
        for (int i = 0; i < slotCount; i++)
        {
            // 그 슬롯의 스크립트를 가져온다.
            Slot slot = AllSlot[i].GetComponent<Slot>();

            // 슬롯이 비어있으면 통과.
            if (!slot.isSlots())
                continue;

            // 슬롯에 존재하는 아이템의 타입과 넣을려는 아이템의 타입이 같고.
            // 슬롯에 존재하는 아이템의 겹칠수 있는 최대치가 넘지않았을 때. (true일 때)
            if (slot.ItemReturn().type == item.type && slot.ItemMax(item))
            {
                // 슬롯에 아이템을 넣는다.
                slot.AddItem(item);
                return true;
            }
        }

        // 빈 슬롯에 아이템을 넣기위한 검사.
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = AllSlot[i].GetComponent<Slot>();

            // 슬롯이 비어있지 않으면 통과
            if (slot.isSlots())
                continue;

            slot.AddItem(item);
            return true;
        }

        // 위에 조건에 해당되는 것이 없을 때 아이템을 먹지 못함.
        return false;
    }

    // 거리가 가까운 슬롯의 반환.
    public Slot NearDisSlot(Vector3 Pos)
    {
        float Min = 10000f;
        int Index = -1;

        int Count = AllSlot.Count;
        for (int i = 25 - (25 * (1 - GameManager.instance.inven_num)); i < Count - (25 * (1 - GameManager.instance.inven_num)); i++) 
        {
            Vector2 sPos = AllSlot[i].transform.GetChild(0).position;
            float Dis = Vector2.Distance(sPos, Pos);

            if (Dis < Min)
            {
                Min = Dis;
                Index = i;
            }
        }

        if (Min > slotSize)
            return null;

        return AllSlot[Index].GetComponent<Slot>();
    }

    // 아이템 옮기기 및 교환.
    public void Swap(Slot slot, Vector3 Pos, string selectCha)
    {
        Slot FirstSlot = NearDisSlot(Pos);

        // 현재 슬롯과 옮기려는 슬롯이 같으면 함수 종료.
        if (slot == FirstSlot || FirstSlot == null)
        {
            slot.UpdateInfo(true, slot.slot.Peek().DefaultImg);
            return;
        }

        // 가까운 슬롯이 비어있으면 옮기기.
        if (!FirstSlot.isSlots())
        {
            FirstSlot.cha = selectCha;
            slot.cha = null;
            Swap(FirstSlot, slot);
        }
        // 교환.
        else
        {
            int Count = slot.slot.Count;
            Item item = slot.slot.Peek();
            Stack<Item> temp = new Stack<Item>();

            {
                for (int i = 0; i < Count; i++)
                    temp.Push(item);

                slot.slot.Clear();
            }

            string a = FirstSlot.cha;
            string b = slot.cha;

            FirstSlot.cha = b;
            slot.cha = a;
            
            Swap(slot, FirstSlot);

            {
                Count = temp.Count;
                item = temp.Peek();

                for (int i = 0; i < Count; i++)
                    FirstSlot.slot.Push(item);

                FirstSlot.UpdateInfo(true, temp.Peek().DefaultImg);
            }

        }
    }

    // 1: 비어있는 슬롯, 2: 안 비어있는 슬롯.
    void Swap(Slot xFirst, Slot oSecond)
    {
        int Count = oSecond.slot.Count;
        Item item = oSecond.slot.Peek();

        for (int i = 0; i < Count; i++)
        {
            if (xFirst != null)
                xFirst.slot.Push(item);
        }

        if (xFirst != null)
            xFirst.UpdateInfo(true, oSecond.ItemReturn().DefaultImg);

        oSecond.slot.Clear();
        oSecond.UpdateInfo(false, oSecond.DefaultImg);
    }
    
    ////////////////
    // 아이템을 넣기위해 모든 슬롯을 검사.
    public bool testAddItem(Item item,GameObject slotobject)
    {
        Slot slot = AllSlot[AllSlot.IndexOf(slotobject)].GetComponent<Slot>();

        if (slot.isSlots())
        {
            // 슬롯에 존재하는 아이템의 타입과 넣을려는 아이템의 타입이 같고.
            // 슬롯에 존재하는 아이템의 겹칠수 있는 최대치가 넘지않았을 때. (true일 때)
            if (slot.ItemReturn().type == item.type && slot.ItemMax(item))
            {
                // 슬롯에 아이템을 넣는다.
                slot.AddItem(item);
                slot.cha = item.cha;
                return true;
            }
            else
            {
                LastItem[LastItem_name.IndexOf(slot.cha)].SetActive(true);
                LastItem.RemoveAt(LastItem_name.IndexOf(slot.cha));
                LastItem_name.Remove(slot.cha);
                LastItem.Add(item.gameObject);
                LastItem_name.Add(item.name);
                item.gameObject.SetActive(false);
                slot.AddItem(item);
                slot.cha = item.cha;
            }
        }
        else
        {
            slot.AddItem(item);
            return true;
        }
        return false;
    }
}
