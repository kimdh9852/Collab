using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour {
    
    public List<GameObject> NextButton = new List<GameObject>();
    
    public void Awake()
    {
        if (GameManager.instance.clear == true)
        {
            GameManager.instance.clear = false;
            GameManager.instance.clearNum ++;
            
            for (int i = 0; i < GameManager.instance.clearNum; i++)
            {
                NextButton[i].SetActive(true);
            }

        }
    }
}
