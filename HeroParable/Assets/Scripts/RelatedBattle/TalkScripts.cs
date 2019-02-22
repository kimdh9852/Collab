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
        EndScripts = false;
        NowScripts = 0;

        Scripts.Add("0");
        Scripts.Add("1");
        Scripts.Add("2");
        Scripts.Add("3");
    }

    public void Skip()
    {
        ScriptPanel.SetActive(false);
        EndScripts = true;
    }

    public void NextScripts()
    {
        if (NowScripts == Scripts.Count - 1)
        {
            Skip();
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
