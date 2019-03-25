using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveMgr : MonoBehaviour
{
    public GameObject A_U_Setting;
    public void SetActiveA_U_Setting()
    {
        if(!A_U_Setting.activeSelf)
        {
            A_U_Setting.SetActive(true);
        }
        else
        {
            A_U_Setting.SetActive(false);
        }
    }
}
