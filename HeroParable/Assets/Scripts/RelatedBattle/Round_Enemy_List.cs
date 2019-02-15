using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Enemy_List : MonoBehaviour {

    public GameObject[] refEnemy;
    public List<GameObject> refEnemyList = new List<GameObject>();
    public List<int> refEnemyDanger = new List<int>();

    public int count;

    public void ResetStage()
    {
        refEnemyList.Clear();
        refEnemyDanger.Clear();

        count = 0;
    }



}
