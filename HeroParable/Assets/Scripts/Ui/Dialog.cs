using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public string[] sentences;
    public Sprite[] sprites;
    public Sprite[] dialogueWindows;

    public void Awake()
    {
        //TODO DB에서 읽어 스크립트 내용 읽어옴
    }
}
