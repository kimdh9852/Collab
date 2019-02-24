using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiDrag : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {

    public static GameObject beingDragged;
    public RectTransform trans;

    public RectTransform cv;
    public string movetype;
    public float xMax;
    public float yLoc;

    public void Start()
    {
        cv = GameObject.Find("Canvas").GetComponent<RectTransform>();
        trans = transform.GetComponent<RectTransform>();
        xMax = trans.rect.xMax - cv.rect.width;
        yLoc = trans.anchoredPosition.y;       
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (movetype == "Move_y")
        {

        }
        else
        {
            trans.anchoredPosition += new Vector2(eventData.delta.x * Time.deltaTime * 35f, 0f);

            if (trans.anchoredPosition.x >= 0f)
            {
                trans.anchoredPosition = new Vector2(0f, yLoc);
            }
            else if (trans.anchoredPosition.x <= -xMax)
            {
                trans.anchoredPosition = new Vector2(-xMax, yLoc);
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = null;
    }
}