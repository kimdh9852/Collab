using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtnManager : MonoBehaviour
{

    public GameObject OptionPan;

    public void SetOn()
    {
        OptionPan.SetActive(true);
    }
    
    public void ExitBtn()
    {
        OptionPan.SetActive(false);
    }
}
