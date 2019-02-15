using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour {

    public void GoWaitting()
    {
        SceneManager.LoadScene("Waitting");
    }

    public void GoLobbyInBattle()
    {
        StartCoroutine(GameManager.instance.EndStage(true));
        GameManager.instance.isStage = false;
        SceneManager.LoadScene("Lobby");
    }

    public void GoSelectStageInBattle()
    {
        StartCoroutine(GameManager.instance.EndStage(true));
        GameManager.instance.isStage = false;
        SceneManager.LoadScene("SelectStage");
    }
    public void GoDetailChaInfo()
    {
        SceneManager.LoadScene("DetailChaInfo");
    }
    public void GoLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void GoSelectStage()
    {
        SceneManager.LoadScene("SelectStage");
    }
    public void GoSelectStageInSetting()
    {
        SceneManager.LoadScene("SelectStage");
        for (int i = 1; i < 2; i++)
        {
            GameObject.Find("Inventory" + i).GetComponent<Inventory>().AllSlot.Clear();
        }
    }
    
    public void GoSetting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void GoBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void GoMarket()
    {
        SceneManager.LoadScene("Market");
    }

    public void GoChaInfo()
    {
        SceneManager.LoadScene("ChaInfo");
    }
}