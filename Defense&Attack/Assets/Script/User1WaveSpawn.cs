using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User1WaveSpawn : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    // 나중에 30초였나? 여튼 그걸로 바꿀 예정
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    //웨이브 불러드림 
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    //웨이브 올리면서 적생성하는 것을 호출
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < ElementBtnManager.AllH; i++)
        {
            SpawnEnemy();
            if (i == ElementBtnManager.AllH-1)
            {
                ElementBtnManager.H1 = 0;
                ElementBtnManager.H2 = 0;
                ElementBtnManager.H3 = 0;
                ElementBtnManager.H4 = 0;
                ElementBtnManager.H5 = 0;
                ElementBtnManager.H6 = 0;
                ElementBtnManager.H7 = 0;
                ElementBtnManager.H8 = 0;
                ElementBtnManager.AllH = 0;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    //실질적으로 적 생성
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
