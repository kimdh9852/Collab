using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetect : MonoBehaviour {

    public GameObject SceneMover;

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneMover.GetComponent<SceneMove>().GoLobby();
        }        
    }
}
