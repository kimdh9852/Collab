using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PhotonInit : MonoBehaviour
{
    // App version
    public string version = "v1.0";
    //player name 
    public InputField userId;
    //Room name
    public InputField roomName;
    public GameObject scrollContents;
    public GameObject roomItem;

    void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }
        userId.text = GetUserId();
        roomName.text = "ROOM_" + Random.Range(0, 999).ToString("000");
    }

    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby !");
        userId.text = GetUserId();
        //PhotonNetwork.JoinRandomRoom();
    }
    
    IEnumerator LoadBattleField()
    {
        //씬을 이동하는 동안 메세지를 차단
        //PhotonNetwork.isMessageQueueRunning = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync("Waiting");
        yield return ao;
    }

    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("User_ID");

        if(string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + Random.Range(0, 999).ToString("000");
        }
        return userId;
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No rooms !");
        //룸 생성 
        PhotonNetwork.CreateRoom("MyRoom");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Enter Room");
        StartCoroutine(this.LoadBattleField());
    }

    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.playerName = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnClickCreateRoom()
    {
        string _roomName = roomName.text;

        if (string.IsNullOrEmpty(roomName.text))
        {
            _roomName = "ROOM_" + Random.Range(0, 999).ToString("000");
        }
            //로컬 플레이어의 이름을 설정
            PhotonNetwork.playerName = userId.text;
            //플레이어 이름을 저장
            PlayerPrefs.SetString("USER_ID", userId.text);

            //생성할 룸의 조건 설정
            RoomOptions Roomoption = new RoomOptions
            {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = 2
            };

            //지정한 조건에 맞는 룸 생성 함수
            PhotonNetwork.CreateRoom(_roomName, Roomoption, TypedLobby.Default);
            Debug.Log(_roomName);      
    }

    void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
    }

    void OnReceivedRoomListUpdate()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }

        int rowCount = 0;
        scrollContents.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        foreach(RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(_room.Name);
            GameObject room = (GameObject)Instantiate(roomItem);
            room.transform.SetParent(scrollContents.transform, false);

            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.Name;
            roomData.connectPlayer = _room.PlayerCount;
            roomData.maxPlayer = _room.MaxPlayers;

            roomData.DispRoomDate();

            //RoomItem의 Button 컴포넌트에 클릭 이벤트를 동적으로 연결
            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnClickRoomItem(roomData.roomName); });
        }

        scrollContents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
        scrollContents.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20);
        //scrollContents.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
    }

    void OnClickRoomItem(string roomName)
    {

        PhotonNetwork.playerName = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);
        PhotonNetwork.JoinRoom(roomName);

    }
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

 
}
