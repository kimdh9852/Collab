using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //public string nowScene;
    public bool isStage = false;

    //LobbySceneUse

    //StageSelcetSceneUse
    public bool clear = false;
    public int clearNum = 1;

    //SettingSceneUse 
    public List<List<GameObject>> AllSlot = new List<List<GameObject>>();
    public List<List<Vector2>> vec2 = new List<List<Vector2>>();
    public List<List<string>> chaList = new List<List<string>>();

    public List<string> EnemyUnit = new List<string>();
    public List<int> EnemyCount = new List<int>();

   //public List<int> TeamLv = new List<int>();
    public List<int> EnemyLv = new List<int>() ;
    public int inven_num = 0;

    //BattleSceneUse
    public int enemycount;
    public int unitcount;
    public RectTransform EndGame;
    public Inventory Iv;
    public SlotCreate Manage_creatslot;

    public void SetCount(int count, string tag)
    {
        if (tag == "Player")
            unitcount = count;
        else
            enemycount = count;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }
    //팀 캐릭터 배치 정보 저장
    public void SettingCheck()
    {
        for (int j = 0; j < 2; j++)
        {
            int slotCount = AllSlot[j].Count;
            vec2.Add(new List<Vector2>());
            chaList.Add(new List<string>());
            for (int i = 0; i < slotCount; i++)
            {
                // 그 슬롯의 스크립트를 가져온다.
                Slot slot = AllSlot[j][i].GetComponent<Slot>();

                // 슬롯이 차있으면 저장
                if (slot.isSlots())
                {
                    vec2[j].Add(new Vector2(slot.xloc, slot.yloc));
                    chaList[j].Add(slot.cha);
                    //TeamLv.Add(slot.lv);
                }
            }
        }
        Iv = GameObject.Find("Inventory" + 1).GetComponent<Inventory>();
        Manage_creatslot = GameObject.Find("SlotManager").GetComponent<SlotCreate>();
    }

    public void TestCheck()
    {
        for (int i = 0; i < EnemyUnit.Count; i++)
        {
            enemycount += EnemyCount[i];
        }

        EndGame = GameObject.Find("EndImage").GetComponent<RectTransform>();

        StartCoroutine(EndStage());
    }

    public IEnumerator EndStage(bool quit = false)
    {
        Time.timeScale = 1.0f;
        while (true)
        {
            if ((isStage && (enemycount == 0 || unitcount == 0 || TimeCheck.second_13 < 0f)) || quit)
            {
                if (!quit)
                {
                    EndGame.anchoredPosition = new Vector3(0f, 0f, 5f);

                    if (enemycount == 0 || TimeCheck.second_13 < 0f)
                    {
                        EndGame.GetChild(0).GetComponent<Text>().text = "Victory!";
                        clear = true;

                        for (int j = 0; j < 2; j++)
                        {
                            for (int i = 0; i < chaList[j].Count; i++)
                            {
                                GameObject.Find(chaList[j][i]).GetComponent<Info>().exp += 10;
                            }
                        }
                    }

                    if (unitcount == 0)
                    {
                        EndGame.GetChild(0).GetComponent<Text>().text = "Lose...";
                    }

                    EndGame = null;
                }

                //nowScene = "Lobby";

                isStage = false;

                AllSlot.Clear();
                chaList.Clear();
                vec2.Clear();
                EnemyUnit.Clear();
                EnemyCount.Clear();
                //TeamLv.Clear();
                EnemyLv.Clear();
                Iv.AllSlot.Clear();
                Manage_creatslot.MyCha.Clear();
                Manage_creatslot.MyHero.Clear();

                enemycount = 0;
                unitcount = 0;
                inven_num = 0;

                GameObject.Find("SpawnManager").GetComponent<Round_Enemy_List>().ResetStage();

                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}