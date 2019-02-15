using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Background : MonoBehaviour
{


    public GameObject background;
    public GameObject Character1;

    // Use this for initialization
    void Start()
    {
        background = GameObject.Find("background");
        Character1 = GameObject.Find("용사");

    }

    // Update is called once per frame
    void Update()
    {

    }
}