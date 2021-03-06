﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User1WaveSpawn : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    
    //정보를 모아오기 위한 공격 유닛 세팅 슬롯들
    public GameObject[] AllSlots;

    //배치된 유닛들
    public List<string> chaList = new List<string>();

    //유닛 보관함
    public Dictionary<string, GameObject> AllPrefabs = new Dictionary<string, GameObject>();
    public GameObject 검병;
    public GameObject 기마병;
    public GameObject 방패병;
    public GameObject 궁병;
    public GameObject 마법사;
    public GameObject 사제;
    public GameObject 창병;
    public GameObject 트레뷰셋;
    
    public void Awake()
    {
        AllPrefabs.Add("검병", 검병);
        AllPrefabs.Add("기마병", 기마병);
        AllPrefabs.Add("방패병", 방패병);
        AllPrefabs.Add("궁병", 궁병);
        AllPrefabs.Add("마법사", 마법사);
        AllPrefabs.Add("사제", 사제);
        AllPrefabs.Add("창병", 창병);
        AllPrefabs.Add("트레뷰셋", 트레뷰셋);
    }

    private void Start()
    {
        //일단 임의로 Start에 넣어둠
        //일단 30개의 슬롯을 전부 ""로 비워두고 시작
        for (int i = 0; i < 30; i++)
        {
            chaList.Add("");
        }
        //SettingCheck();
    }

    //제한시간 30초가 다되면 함수가 불러오도록 하기
    public void SettingCheck()
    {
        AllSlots = GameObject.FindGameObjectsWithTag("SettingSlot");

        int slotCount = AllSlots.Length;//30
        for (int i = 0; i < slotCount; i++)
        {
            // 그 슬롯의 스크립트를 가져온다.
            A_U_SettingSlot slot = AllSlots[i].GetComponent<A_U_SettingSlot>();

            // 슬롯이 차있으면 저장
            if (slot.isSlots())
            {
                chaList[i] = slot.unit_Name;
            }
        }
        StartCoroutine(SpwanAttackUnit(chaList));
    }

    IEnumerator SpwanAttackUnit(List<string> unitname)
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.5f);

            if (unitname[i] != "")
                Instantiate(AllPrefabs[unitname[i]], spawnPoint.position, spawnPoint.rotation);
        }
        GameObject.Find("TurnManager").GetComponent<TurnManager>().spawn = true;
    }




    //// 나중에 30초였나? 여튼 그걸로 바꿀 예정
    //public float timeBetweenWaves = 5f;
    //private float countdown = 2f;

    ////웨이브 불러드림 
    //void Update()
    //{
    //    if (countdown <= 0f)
    //    {
    //        StartCoroutine(SpawnWave());
    //        countdown = timeBetweenWaves;
    //    }
    //    countdown -= Time.deltaTime;
    //}

    ////웨이브 올리면서 적생성하는 것을 호출
    //IEnumerator SpawnWave()
    //{
    //    for (int i = 0; i < ElementBtnManager.AllH; i++)
    //    {
    //        SpawnEnemy();
    //        if (i == ElementBtnManager.AllH-1)
    //        {
    //            ElementBtnManager.H1 = 0;
    //            ElementBtnManager.H2 = 0;
    //            ElementBtnManager.H3 = 0;
    //            ElementBtnManager.H4 = 0;
    //            ElementBtnManager.H5 = 0;
    //            ElementBtnManager.H6 = 0;
    //            ElementBtnManager.H7 = 0;
    //            ElementBtnManager.H8 = 0;
    //            ElementBtnManager.AllH = 0;
    //        }
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

    ////실질적으로 적 생성
    //void SpawnEnemy()
    //{
    //    Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    //}
}
