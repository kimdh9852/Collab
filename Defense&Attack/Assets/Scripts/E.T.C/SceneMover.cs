using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public void SceneMove(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
