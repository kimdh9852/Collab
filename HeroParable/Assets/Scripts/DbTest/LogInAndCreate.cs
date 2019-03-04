using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInAndCreate : MonoBehaviour
{
    [Header("Login")]
    public InputField Id;
    public InputField Password;
    public string userid;
    public string userpassword;
    public string username;

    [Header("CreateId")]
    public GameObject CreatePanel;
    public InputField C_id;
    public InputField C_PassWord;
    public InputField C_Name;
    public string CU_userid;
    public string CU_userpassword;
    public string CU_username;

    public GameObject FailedPanel;
    
    void Update()
    {
        userid = Id.text;
        userpassword = Password.text;
        CU_userid = C_id.text;
        CU_userpassword = C_PassWord.text;
        CU_username = C_Name.text;
    }

    public void Login()
    {
        StartCoroutine(Co_login());
    }
    
    public void CreateUser()
    {
        StartCoroutine(Co_CreateUser());
    }

    IEnumerator Co_login()
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", userid);
        form.AddField("PassWord", userpassword);

        WWW www = new WWW("http://localhost/heroparable/login.php", form);

        yield return www;

        if (www.text == "OK")
        {
            GameObject.Find("SceneMover").GetComponent<SceneMove>().GoLobby();
        }
        else
        {
            Debug.Log(www.text);

            FailedPanel.SetActive(true);

            if (www.text == "ID")
            {
                FailedPanel.transform.GetChild(0).GetComponent<Text>().text = "존재하지 않는 ID입니다.";
            }
            else
            {
                FailedPanel.transform.GetChild(0).GetComponent<Text>().text = "잘못된 PassWord입니다.";
            }
        }
    }

    IEnumerator Co_CreateUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", CU_userid);
        form.AddField("PassWord", CU_userpassword);
        form.AddField("Name", CU_username);

        WWW www = new WWW("http://localhost/heroparable/createid.php", form);

        yield return www;

        if (www.text == "OK")
        {
            Debug.Log(www.text);
            GameObject.Find("SceneMover").GetComponent<SceneMove>().GoLobby();
        }
        else
        {
            Debug.Log(www.text);

            FailedPanel.SetActive(true);

            if (www.text == "Id")
            {
                FailedPanel.transform.GetChild(0).GetComponent<Text>().text = "존재하는 ID입니다.";
            }
            else if (www.text == "Name")
            {
                FailedPanel.transform.GetChild(0).GetComponent<Text>().text = "존재하는 Name입니다.";
            }
        }
    }

    public void Confirm(string panname)
    {
        if (panname == "fail")
        {
            FailedPanel.SetActive(false);
        }
        else
        {
            CreatePanel.SetActive(false);
        }
    }
    
    public void OnCreatePanel()
    {
        CreatePanel.SetActive(true);
    }
}
