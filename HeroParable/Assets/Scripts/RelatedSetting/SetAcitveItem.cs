using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAcitveItem : MonoBehaviour {

    public Dictionary<string,GameObject> ItemList = new Dictionary<string, GameObject>();

    public void SetOn(string cha)
    {
        ItemList[cha].SetActive(true);
    }
}
