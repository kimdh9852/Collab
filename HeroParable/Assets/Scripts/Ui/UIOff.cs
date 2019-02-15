using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOff : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

    public bool isEnter = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && !isEnter)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }
}
