using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingAttackUnits : MonoBehaviour
{
    public enum TYPE { Attack, Defense }

    public TYPE type;           // 유닛의 타입.
    public Sprite DefaultImg;   // 기본 이미지.
    //public int MaxCount;        // 겹칠수 있는 최대 숫자. 안쓸듯
    public string UnitName;

    // 인벤토리에 접근하기 위한 변수.
    private Inventory Iv;

    void Awake()
    {
        // 반환된 객체가 가지고 있는 스크립트를 GetComponent를 통해 가져온다.
        Iv = GameObject.Find("AttackInventory1").GetComponent<Inventory>();
    }

    void AddItem()
    {
        Iv.AddItemAttack(this);
    }

    public void UnitOnClick()
    {
        AddItem();
    }
    public void OnMouseDown()
    {
        UnitOnClick();
    }
}