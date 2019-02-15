using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMgr : MonoBehaviour {

    public GameObject Cha1;
    public GameObject Cha2;

    public Image Iv1;
    public Image Iv2;

    public GameObject[] Ids;
    public GameObject sel_ids;
    public List<GameObject> _Id = new List<GameObject>();

    void Start()
    {
        Ids = GameObject.FindGameObjectsWithTag("Slot");

        sel_ids = GameObject.Find("HeroList");

        foreach(GameObject Id in Ids)
        {
            _Id.Add(Id);
        }
    }

    public void OnIV1()
    {
        Iv1.rectTransform.anchoredPosition = new Vector3(0f, 0f, 0f);
        Iv2.rectTransform.anchoredPosition = new Vector3(-555f, 0f, 0f);

        GameManager.instance.inven_num = 0;
        for (int i = 0; i < _Id.Count; i++)
        {
            _Id[i].GetComponent<ItemDrag>().Iv = GameObject.Find("Inventory" + 
                (GameManager.instance.inven_num + 1).ToString()).GetComponent<Inventory>();
        }
        
    }

    public void OnIV2()
    {
        Iv1.rectTransform.anchoredPosition = new Vector3(-555f, 0f, 0f);
        Iv2.rectTransform.anchoredPosition = new Vector3(0f, 0f, 0f);

        GameManager.instance.inven_num = 1;
        for (int i = 0; i < _Id.Count; i++)
        {
            _Id[i].GetComponent<ItemDrag>().Iv = GameObject.Find("Inventory" + 
                (GameManager.instance.inven_num + 1).ToString()).GetComponent<Inventory>();
        }
        
    }

    public void OnCha1()
    {
        Cha1.SetActive(true);
        Cha2.SetActive(false);
    }

    public void OnCha2()
    {
        Cha1.SetActive(false);
        Cha2.SetActive(true);
    }
    
}
