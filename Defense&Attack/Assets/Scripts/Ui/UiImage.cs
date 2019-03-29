using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiImage : MonoBehaviour
{

    public SpriteRenderer sp;
    public Image img;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        img = GetComponent<Image>();
    }

    void Update()
    {
        img.sprite = sp.sprite;
    }
}
