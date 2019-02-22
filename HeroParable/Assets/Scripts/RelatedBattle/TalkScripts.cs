using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkScripts : MonoBehaviour
{
    public bool EndScripts;

    public Text ScriptText;
    public GameObject ScriptPanel;

    public List<string> Scripts = new List<string>();
    public int NowScripts;

    public void Awake()
    {
        Time.timeScale = 0.0f;
        EndScripts = false;
        NowScripts = 0;

        Scripts.Add("누구냐");
        Scripts.Add("누구냐고 묻는다면 대답해주는게 인지상정");
        Scripts.Add("나는 로사");
        Scripts.Add("나는 로이");
        Scripts.Add("나는 냐용이다아옹");

        ScriptText.text = Scripts[0];

    }

    public void Skip()
    {
        ScriptPanel.SetActive(false);
        EndScripts = true;
        Time.timeScale = 1.0f;
    }

    public void NextScripts()
    {
        if (NowScripts == Scripts.Count - 1)
        {
            Skip();
            return;
        }

        NowScripts++;
        ScriptText.text = Scripts[NowScripts];
    }

    public void PreviousScripts()
    {
        if (NowScripts == 0)
            return;

        NowScripts--;
        ScriptText.text = Scripts[NowScripts];
    }
}
