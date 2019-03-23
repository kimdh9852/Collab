using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : Photon.MonoBehaviour
{
    public Text txtConnect;
    public GameObject playerPrefab;
    private PhotonPlayer player1;
    private PhotonPlayer player2;
    public bool GameStart;

    void Awake()
    {
        GameStart = false;
        PhotonNetwork.isMessageQueueRunning = true;
        GetConnectPlayerCount();
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            player1 = player;
            Debug.Log("player1.ID : " + player1.ID);
        }
    }

    public void OnClickCreateCube()
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
    }

    void GetConnectPlayerCount()
    {
        Room currRoom = PhotonNetwork.room;
        txtConnect.text = currRoom.PlayerCount.ToString() + "/" + currRoom.MaxPlayers.ToString();
    }
    
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        player2 = newPlayer;
        GetConnectPlayerCount();
        Room currRoom = PhotonNetwork.room;
    }

    //ready 
    public void OnClickReady()
    {
        SceneManager.LoadSceneAsync("Battle");
        //if (player2.ID > 1 && photonView.isMine)
        //{
        //    GameStart = true;
        //}
        //if (photonView.isMine && GameStart)
        //{
        //    GoBattle();
        //}
    }

    void GoBattle()
    {
        SceneManager.LoadScene("Battle");
    }

    void OnPhotonPlayerDiscconnected(PhotonPlayer outPlayer)
    {
        GetConnectPlayerCount();
        PhotonNetwork.LeaveRoom();
    }

    public void OnClickExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
