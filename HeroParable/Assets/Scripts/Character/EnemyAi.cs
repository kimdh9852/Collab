/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : Round_Enemy_List
{

    public Transform target;
    public Info info;

    int min;
    //int max;
    public int count;
    //int location;

    Vector3 vector;
    public Animator animator;

    public bool isAttack = true;

    public string jobName;

    public int delay;

    public enum State { Nomal, Stun, Silence, Taunt, Fire, NumberofType };
    public ArrayList _state = new ArrayList();

    void Start()
    {
        for (int i = 0; i < (int)State.NumberofType; i++)
        {
            if (i == 0)
                _state.Add(true);
            else
                _state.Add(false);
        }
        info = GetComponent<Info>();
        animator = GetComponentInChildren<Animator>();
        GameManager.instance.isStage = true;
        SetDanger();
        Settarget();
        vector = (target.position - transform.position).normalized;
        GameManager.instance.unitcount = playerUnit.Length;
        delay = 0;
    }

    public void Recovery(float time)
    {
        StartCoroutine(Cor_recovery(time));
    }

    IEnumerator Cor_recovery(float time)
    {
        yield return new WaitForSeconds(time);
        _state[0] = true;
        for (int i = 1; i < (int)State.NumberofType; i++)
            _state[i] = false;
    }

    void Update()
    {
        delay++;
        setFlipX();


        if (delay > 250 && count >= 1)
            if (!(bool)_state[(int)State.Stun])
                Move();

        if (count >= 1 && !(bool)_state[(int)State.Silence])
        {
            //int percentage = Random.Range(0, 100);

            if (isAttack && count != 0)
                if (!(bool)_state[(int)State.Stun])
                    StartCoroutine(Attack());
        }

        if ((bool)_state[(int)State.Fire])
            if (isAttack)
                StartCoroutine(fire());

        //침묵
        if ((bool)_state[(int)State.Silence])
        {
            if (isAttack && count != 0)
                StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        float damage = 0;
        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange)
        {
            isAttack = false;

            animator.SetTrigger(jobName + "Attack");
            if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
            {
                if (Random.Range(1, 1000) < info.Critcal * 10)
                {
                    damage = info.Power * info.CD - target.GetComponent<Info>().PhyArmor;
                    Debug.Log("Critical!!");
                }
                else
                {
                    damage = info.Power - target.GetComponent<Info>().PhyArmor;
                }

                target.gameObject.GetComponent<Info>().Hp -= damage;

                /*
                //상태이상 공격 로직
                target.gameObject.GetComponent<Ai>()._state[(int)State.Stun] = true;
                target.gameObject.GetComponent<Ai>().Recovery(5f);
                _state[(int)State.Stun] = true;
                _state[(int)State.Fire] = true;
                *//*
                Debug.Log(target.gameObject.name + " / " + target.gameObject.GetComponent<Info>().Hp);
            }
            else
            {
                Debug.Log("적 회피!");
            }


            if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
            {
                if (target.gameObject.activeSelf)
                {
                    playerUnitDanger.Remove(playerUnitDanger[playerUnitList.IndexOf(target.gameObject)]);
                    playerUnitList.Remove(target.gameObject);
                    target.gameObject.SetActive(false);
                }
                //enemyDanger.RemoveAt(location);
                target = null;
                SetDanger();
                Settarget();
                if (target != null)
                    vector = (target.position - transform.position).normalized;
            }

            yield return new WaitForSeconds(info.AttackSpeed);
            isAttack = true;
        }

    }

    void Move()
    {
        if (target == null)
            return;

        if (Vector2.Distance(transform.position, target.position) > info.AttackRange)
            transform.position += new Vector3(vector.x, vector.y, vector.y * 10f) * 3f * Time.deltaTime;
    }

    int Selectmin(int mindistance, int newdistance)
    {
        if (Vector2.Distance(transform.position, playerUnitList[mindistance].transform.position) > Vector2.Distance(transform.position, playerUnitList[newdistance].transform.position))
            mindistance = newdistance;

        return mindistance;
    }

    int SelectDangerMax(int MaxDanger, int newDanger)
    {
        if (playerUnitDanger[MaxDanger] < playerUnitDanger[newDanger])
            MaxDanger = newDanger;
        else if (playerUnitDanger[MaxDanger] == playerUnitDanger[newDanger] && MaxDanger != newDanger)
            return -1;

        return MaxDanger;
    }

    Transform Settarget()
    {

        if (playerUnit.Length == 0)
        {
            return null;
        }

        if (playerUnitList.Count == 0)
        {
            foreach (GameObject _unit in playerUnit)
            {
                playerUnitList.Add(_unit);
                playerUnitDanger.Add(_unit.GetComponent<Info>().Danger);
            }
        }

        if (playerUnit != null)
        {
            if (playerUnitList.Count == 1)
            {
                target = playerUnitList[0].transform;
            }
            else
            {
                min = 0;
                //max = 0;
                for (int i = 0; playerUnitList.Count > i; i++)
                {
                    target = playerUnitList[Selectmin(min, i)].transform;
                    /*
                    if (SelectDangerMax(max, i) == -1)
                    {
                        target = playerUnitList[Selectmin(min, i)].transform;
                        //Debug.Log(enemyDanger[SelectDangerMax(max, i)]);
                        //location = Selectmin(min, i);
                    }
                    else
                    {
                        target = playerUnitList[Selectmin(min, i)].transform;
                        //target = playerUnitList[SelectDangerMax(max, i)].transform;
                        //Debug.Log(enemyDanger[SelectDangerMax(max, i)]);
                        //location = SelectDangerMax(max, i);
                    }
                    *//*
                }
            }
        }

        return target;
    }

    IEnumerator fire()
    {
        if ((bool)_state[(int)State.Fire])
            info.Hp -= 100;
        isAttack = false;
        yield return new WaitForSeconds(5f);
        isAttack = true;
    }

    void SetDanger()
    {
        playerUnit = GameObject.FindGameObjectsWithTag("Player");
        count = playerUnit.Length;
        GameManager.instance.unitcount = count;


        if (playerUnit.Length == 0)
        {
            count = 0;
            GameManager.instance.unitcount = count;
            return;
        }

        if (playerUnitList.Count == 0)
        {
            foreach (GameObject _enemy in playerUnit)
            {
                playerUnitList.Add(_enemy);
                playerUnitDanger.Add(_enemy.GetComponent<Info>().Danger);
            }
        }
    }

    void setFlipX()
    {
        if (target == null)
            return;
        if (target.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
    }

}*/