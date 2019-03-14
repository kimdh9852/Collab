using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type
{
    canPurchase,
    cantPurchase,
    onlyUser1,
    onlyUser2,
    castle,
    road
}

public class Tile : MonoBehaviour
{

    static public bool btn_state = false;
    public Type type;
    public Button yes;
    public Button no;
    public Camera camera1;
    public Vector3 target;
    public Vector3 screenPos;
    public GameObject turret;
    public Vector3 positionOffset;

    TileBtnManager Tilemanage;

    //마우스를 클릭하면
    void OnMouseDown()
    {
        if (GameObject.Find("Canvas").GetComponent<UIHover>().isUIHover)
            return;

        btn_state = false;
        Tilemanage = GameObject.Find("TileBtnManager").GetComponent<TileBtnManager>();
        //타일의 태그가 Creep이면 중립이라 땅을 살지말지 결정 Yes와 No창은 위치 이동만 함
        if (this.gameObject.CompareTag("Creep"))
        {
            Tilemanage.zone = this.gameObject;
            target = this.transform.position;
            screenPos = camera1.WorldToScreenPoint(target);
            float x = screenPos.x;
            yes.transform.position = new Vector3(x + 40, screenPos.y + 20, yes.transform.position.z);
            no.transform.position = new Vector3(x + 40, screenPos.y, yes.transform.position.z);
        }
        // 타일의 태그가 User1Area이면.. 원소 조합 창 끄기, 태그 Full로 교체 , 터렛 생성 
        if(this.gameObject.CompareTag("User1Area"))
        {
            ElementBtnManager.Btn_chang = false;
            this.gameObject.tag = "Full";
            Instantiate(turret, transform.position + positionOffset, transform.rotation);
        }
    }
    void Update()
    {
        // yes or no 둘 중 하나를 선택했을 때 버튼을 다른 곳으로 이동시켜줌 
        if(btn_state)
        {
            yes.transform.position = new Vector3(0, -239.9f, 0);
            no.transform.position = new Vector3(0, -239.9f, 0);
        }
    }
}
