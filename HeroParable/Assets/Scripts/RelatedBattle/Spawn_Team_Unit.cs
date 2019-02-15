using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Team_Unit : MonoBehaviour
{
    public GameObject Knight;
    public GameObject 보르카대검병;
    public GameObject 보르카기사;
    public GameObject 보르카경비대;
    public GameObject 배월교비밀신자;
    public GameObject 냉혹한칼잡이;
    public GameObject 혹독한활잡이;
    public GameObject 수행중인리자드맨;
    public GameObject 왕국병사;
    public GameObject 임플린열기구병;
    public GameObject 청동기계골렘;

    public Dictionary<string, GameObject> chaDic = new Dictionary<string, GameObject>();

    bool spawncheck = true;

    private void Awake()
    {
        chaDic.Add("Knight", Knight);
        chaDic.Add("보르카대검병", 보르카대검병);
        chaDic.Add("보르카기사", 보르카기사);
        chaDic.Add("보르카경비대", 보르카경비대);
        chaDic.Add("배월교비밀신자", 배월교비밀신자);
        chaDic.Add("냉혹한칼잡이", 냉혹한칼잡이);
        chaDic.Add("혹독한활잡이", 혹독한활잡이);
        chaDic.Add("청동기계골렘", 청동기계골렘);
        chaDic.Add("임플린열기구병", 임플린열기구병);
        chaDic.Add("왕국병사", 왕국병사);
        chaDic.Add("수행중인리자드맨", 수행중인리자드맨);

        StartCoroutine(Spawn_unit(5f));
    }

    IEnumerator Spawn_unit(float time = 0f)
    {
        for (int j = 0; j < 2;)
        {
            if (spawncheck)
            {
                spawncheck = false;
                for (int i = 0; i < GameManager.instance.chaList[j].Count; i++)
                {
                    // Count 넣는 유닛 갯수
                    GameObject player = Instantiate(chaDic[GameManager.instance.chaList[j][i]],
                    new Vector3(GameManager.instance.vec2[j][i].x - 10, -GameManager.instance.vec2[j][i].y, -50f + i * 5),
                    Quaternion.identity);
                    //player.GetComponent<Info>().GetInfoData(GameManager.instance.TeamLv[i]);
                    player.name = GameManager.instance.chaList[j][i];
                    //Debug.Log(GameManager.instance.chaList[j][i]);
                    //[][] 앞쪽은 그냥 주구장창 0만.. 뒤쪽이 이름... 앞쪽은 0 knight 

                    /*Debug.Log("레벨 " + GameManager.instance.TeamLv[i] + "의 " +
                         player.name + "가 " + "소환됨");*/
                }

                yield return new WaitForSeconds(time);
                spawncheck = true;
                j++;
            }
        }
    }

} 