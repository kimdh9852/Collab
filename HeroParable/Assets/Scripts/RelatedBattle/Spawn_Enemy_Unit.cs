using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy_Unit : MonoBehaviour {

    public Dictionary<string,GameObject> eL = new Dictionary<string,GameObject>();

    public GameObject Skelleton_Small;
    public GameObject Skelleton_Normal;
    public GameObject Skelleton_Big;
    public GameObject Skelleton_Warrior;
    public GameObject Skelleton_Archer;
    public GameObject Skelleton_Rogue;
    public GameObject Skelleton_Mage;
    public GameObject Skelleton_HeadHunter;
    public GameObject Skelleton_SpearMan;
    public GameObject Skelleton_SwordMan;
    public GameObject AdinsSuha;
    public GameObject DeadCrow;

    private void Awake()
    {
        eL.Add("Skelleton_Small", Skelleton_Small);
        eL.Add("Skelleton_Normal", Skelleton_Normal);
        eL.Add("Skelleton_Big", Skelleton_Big);
        eL.Add("Skelleton_Warrior", Skelleton_Warrior);
        eL.Add("Skelleton_Archer", Skelleton_Archer);
        eL.Add("Skelleton_Rogue", Skelleton_Rogue);
        eL.Add("Skelleton_Mage", Skelleton_Mage);
        eL.Add("Skelleton_HeadHunter", Skelleton_HeadHunter);
        eL.Add("Skelleton_SpearMan", Skelleton_SpearMan);
        eL.Add("Skelleton_SwordMan", Skelleton_SwordMan);
        eL.Add("Adin'sSuha", AdinsSuha);
        eL.Add("DeadCrow", DeadCrow);

        if(ButtonManager.StageNumber!=0)
        {
            for (int i = 0; i < GameManager.instance.EnemyUnit.Count; i++)
            {
                for (int a = 0; a < GameManager.instance.EnemyCount[i]; a++)
                {
                    GameObject enemy = Instantiate(eL[GameManager.instance.EnemyUnit[i]],
                    new Vector3(10f + Random.Range(-3, 3), -2f + Random.Range(-3, 3), -50f),
                    Quaternion.Euler(0f, -180f, 0f));
                    enemy.GetComponent<Info>().GetInfoData(GameManager.instance.EnemyLv[i]);
                    enemy.name = GameManager.instance.EnemyUnit[i];
                    Debug.Log("레벨 " + GameManager.instance.EnemyLv[i] + "의 " +
                        enemy.name + "가 " + GameManager.instance.EnemyCount[i] + "마리 소환됨");
                }
            }
        }

        //for (int i = 0; i < GameManager.instance.EnemyUnit.Count; i++)
        //{
        //    for (int a = 0; a < GameManager.instance.EnemyCount[i]; a++)
        //    {
        //        GameObject enemy = Instantiate(eL[GameManager.instance.EnemyUnit[i]],
        //        new Vector3(10f + Random.Range(-3, 3), -2f + Random.Range(-3, 3), -50f),
        //        Quaternion.Euler(0f, -180f, 0f));
        //        enemy.GetComponent<Info>().GetInfoData(GameManager.instance.EnemyLv[i]);
        //        enemy.name = GameManager.instance.EnemyUnit[i];
        //        Debug.Log("레벨 " + GameManager.instance.EnemyLv[i] + "의 " +
        //            enemy.name + "가 " + GameManager.instance.EnemyCount[i] + "마리 소환됨");
        //    }
        //}

        GameManager.instance.TestCheck();
    }
}
