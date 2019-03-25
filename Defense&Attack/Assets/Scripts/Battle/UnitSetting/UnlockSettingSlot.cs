using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSettingSlot : MonoBehaviour
{
    //x를 누르면 canUse가 트루로 바뀌면서 세팅할 수 있도록 바꾸는 스크립트
    //TODO 골드와 연계해서 돈이 모자르면 리턴 충분하면 구입하기 창이 뜨면서 최종확인
    public void OnDown()
    {
        transform.GetComponentInParent<A_U_SettingSlot>().canUse = true;
    }
}
