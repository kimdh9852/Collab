﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//배치창에 넣기 위한 스크립트 인벤토리중 아이템에 해당하는 것
public class Units : MonoBehaviour
{
    public enum TYPE { Attack, Defense }

    public TYPE type;           // 유닛의 타입.
    public Sprite DefaultImg;   // 기본 이미지.
    //public int MaxCount;        // 겹칠수 있는 최대 숫자. 안쓸듯
    
    
    // 인벤토리에 접근하기 위한 변수.
    private AttackUnitSetting Iv;

    void Awake()
    {
        // 반환된 객체가 가지고 있는 스크립트를 GetComponent를 통해 가져온다.
        Iv = GameObject.Find("AttackUnitSetting").GetComponent<AttackUnitSetting>();
    }

    void AddItem()
    {
        // 아이템 획득에 실패할 경우.
        if (!Iv.AddItem(this))
            Debug.Log("아이템이 가득 찼습니다.");
        else // 아이템 획득에 성공할 경우.
            gameObject.SetActive(false); // 아이템을 비활성화 시켜준다.
    }

    // AddItem으로 유닛을 배치해야함. 밑의 함수는 수정해야함
    void OnTriggerEnter(Collider _col)
    {
        // 플레이어와 충돌하면.
        if (_col.gameObject.layer == 10)
            AddItem();
    }
}