using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool isUIHover = false;

    public void OnPointerEnter(PointerEventData enentData)
    {
        isUIHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isUIHover = false;
    }
}
