using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{

    public GameObject LobbyPan;

    //public static bool btnIsOpen = false;

    public void SetOnLobby()
    {
        /*
       if (btnIsOpen)
            return;

        btnIsOpen = true;
        */
        LobbyPan.SetActive(true);
    }

    public void ExitBtn()
    {/*
        btnIsOpen = false;
        */
        LobbyPan.SetActive(false);

    }
}
