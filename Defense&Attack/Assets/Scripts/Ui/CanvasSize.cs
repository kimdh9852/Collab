using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSize : MonoBehaviour
{

    public int width = 9;
    public int height = 16;

    void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.width * width / height, true);
    }
}
