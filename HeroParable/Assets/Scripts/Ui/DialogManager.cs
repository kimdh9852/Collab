using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    //스크립트까 끝날때까지 전투 대기 
    public bool endScripts;
    //대화가 나오는 판넬
    public GameObject scriptPanel;
    //대화창
    public Text scriptText;
    //대화와 함께 나올 캐릭터 이미지들 (미구현)
    public SpriteRenderer img_team;
    public SpriteRenderer img_enemy;

    //스크립트 내용 (dialog 에서 받아오며 dialog는 추후 디비에서 받아오도록 수정할 예정)
    public List<string> scripts;
    //대화와 함께 나올 캐릭터 이미지들 (미구현)
    public List<Sprite> sprites;
    //대화진행상황
    public int scriptsCount;
    //대화 목록 및 대화캐릭터 이미지 받아올 스크립트
    public Dialog dialog;

    public void Awake()
    {
        Time.timeScale = 0.0f;
        endScripts = false;
        scripts = new List<string>();
        sprites = new List<Sprite>();
        scriptsCount = 0;
        dialog = GetComponent<Dialog>();
        
        ShowDialog(dialog);
    }

    //TODO입력(터치값)을 받으면 코루틴이 멈추고 바로 스크립트 내용이 보이도록해야함
    public void Update()
    {
       
        //if (endScripts)
        //    return;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{ 
        //    StopAllCoroutines();
        //        scriptText.text = scripts[scriptsCount];
        //}
    }

    public void ShowDialog(Dialog dialog)
    {
        for (int i = 0; i < dialog.sentences.Length; i++)
        {
            scripts.Add(dialog.sentences[i]);
            //sprites.Add(dialog.sprites[i]);
        }

        StartCoroutine(StartDialogueCo());
    }
    
    IEnumerator StartDialogueCo()
    {
        scriptText.text = "";
        //한글자씩 출력
        for (int i = 0; i < scripts[scriptsCount].Length; i++)
        {
            scriptText.text += scripts[scriptsCount][i];
            yield return new WaitForSeconds(0.03f);
        }
        
    }


    public void NextScripts()
    {
        if (scriptsCount == scripts.Count - 1)
        {
            Skip();
            return;
        }

        scriptsCount++;
        StartCoroutine(StartDialogueCo());
    }

    public void PreviousScripts()
    {
        if (scriptsCount == 0)
            return;

        scriptsCount--;
        StartCoroutine(StartDialogueCo());
    }

    public void Skip()
    {
        scriptPanel.SetActive(false);
        endScripts = true;
        Time.timeScale = 1.0f;
    }
}
