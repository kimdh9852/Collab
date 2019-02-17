using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skill_System;

public class Ai : Round_Enemy_List
{
    //todo: 적목록을 관리하는 스크립트나 DB따로 제작
    public Transform target;
    public Info info;

    public bool SkillUse; // 캐릭터가 스킬을 쓴지 안쓴지... 아직 사용중은 아님 

    public int min;
    public int max;

    public float time = -5f; // 5초 후 전투가 시작되기 때문에 -5초부터 센다 
    public float time1 = -7f; 

    //int location;
    //public Unit_Skill skill;
    public Units_Skill Skill_Manager;

    public Vector3 vector;
    public Animator animator;

    public bool isAttack = true;
    string Enemy_tag;

    public GameObject arrow;
    public SpriteRenderer spriteRenderer;

    public int delay = 0; //임시 코드 추후 삭제예정

    public enum State { Nomal, Stun, Silence, Taunt, Fire, NumberofType };
    public ArrayList _state = new ArrayList();

    public static int buttoncheck = 0;
    //skill 과 attack 동시에 일어나는 경우? 허용할지 말지..
    public bool skilluse = false;

    void Start()
    { 
        //skill = new Unit_Skill();
        //skill.Set_Skill_Stat();
        for (int i = 0; i < (int)State.NumberofType; i++)
        {
            if (i == 0)
                _state.Add(true);
            else
                _state.Add(false);
        }
        info = GetComponent<Info>();
        animator = GetComponentInChildren<Animator>();

        if (this.CompareTag("Player"))
            Enemy_tag = "Enemy";
        else
            Enemy_tag = "Player";
        
        refEnemy = GameObject.FindGameObjectsWithTag(Enemy_tag);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        count = refEnemy.Length;
        GameManager.instance.isStage = true;
        SetDanger();
        Settarget();
        vector = (target.position - transform.position).normalized;
        delay = 0;
    }

    void Update()
    {
        delay++;
        setFlipX();

        //쿨타임 시간 체크
        time1 += Time.deltaTime;
        time += Time.deltaTime;
        ActiveSkill();

        if (delay > 250 && count >= 1)
            if (!(bool)_state[(int)State.Stun])
                Move();

        if (count >= 1 && !(bool)_state[(int)State.Silence])
        {
            int percentage = Random.Range(0, 100);

            if (isAttack && count != 0)
                if (!(bool)_state[(int)State.Stun])
                {
                    Attack(percentage);
                    StartSkill(); // 쿨다운 동안 스킬을 사용못함 그외에는 자동으로 스킬 사용 
                }
            return;
        }

        if ((bool)_state[(int)State.Fire])
            if (isAttack)
                StartCoroutine(fire());

        //침묵 
        if (isAttack && count != 0)
            StartCoroutine(Co_Attack());

    }
    
    void Attack(int percent)
    {
        //skill.Skill_use(0);
       StartCoroutine(Co_Attack());
    }
    // asset store에서 사용하는 애니메이션 에러를 막아주는 코드.. 없으면 에러 떠서 보기가 싫다 
    void AttackStart()
    {

    }

    void StartSkill()
    {
         //StartCoroutine(PassiveSkill());
    }

    //액티브 스킬 같은 경우는 미리 올려놓은 스킬을 사용해야 함으로 
    //1. 적용 대상이 아군인지(버프) 적군인지(디버프, 공격)을 파악 
    //2. 스킬 레벨 등을 가져올 수 있는 info 
    //3. 만약 공격이라면 혹은 범위라면 누구에게 대상으로 할지? 어떻게 할지...등..

    public void ActiveSkill() //적은 액티브를 사용 못함 일단은 아군만
    {
        if (buttoncheck == 1)
        {
            //float damage = 0;
            if (Vector2.Distance(transform.position, target.position) <= info.AttackRange) //skillRange를 만들면 바꿔주어야함
            {
                if (this.gameObject.GetComponent<Info>().skiil2 == 2)
                {
                    Debug.Log(this.gameObject.name + "이 '사기진작' 사용하였습니다.");
                    this.gameObject.GetComponent<Info>().skiil2 = 0;
                    time1 = 0;
                }
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    if (target.gameObject.activeSelf)
                    {
                        refEnemyDanger.Remove(refEnemyDanger[refEnemyList.IndexOf(target.gameObject)]);
                        refEnemyList.Remove(target.gameObject);
                        target.GetComponent<Ai>().Die();
                    }
                    target = null;
                    SetDanger();
                    Settarget();
                    if (target != null)
                        vector = (target.position - transform.position).normalized;
                }
            }

        }

        buttoncheck = 0;

        if (time1 >= 3f) // SkillCooldown 쓴직후 예를들어 32초에 사용하면 36초에 다시 사용가능함.. 2,3,4,5,6 의 5초방식
        {
            this.gameObject.GetComponent<Info>().skiil2 = 2;
            //Debug.Log(this.gameObject.name + "10초가 지났습니다");
        }
    } // 아직은... 

    public void Die()
    {
        animator.SetTrigger("Dead");
        StartCoroutine("Co_Die");
    }

    IEnumerator Co_Die()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    IEnumerator PassiveSkill() // 데미지 들어가는거는 아직 안넣었음! 
    {
        float damage = 0;
        // 코루틴 두개 돌리니까 attack으로 죽이는 경우 target이 null이되기 때문에 NullReferenceExcpetion이 떠서 임시로 막아놓음.
        // 해법좀.. 부탁...ㅠ
        if (target == null||transform == null)
        {
            target = info.transform;
        }

        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange )
        {
                isAttack = false;
                animator.SetTrigger("Attack");
                if (this.gameObject.GetComponent<Info>().skiil1 == 1)
                {
                    damage = (int)(info.skiil1 * info.Power * Random.Range(0.98f, 1.02f)) - target.GetComponent<Info>().PhyArmor;
                    //Debug.Log(this.gameObject.name + "이 '일격' 사용하였습니다.");
                    this.gameObject.GetComponent<Info>().skiil1 = 0;
                    target.gameObject.GetComponent<Info>().Hp -= damage;
                    //Debug.Log(this.gameObject.name + " 이 공격하여 " + target.gameObject.name + "의 피가  " + target.gameObject.GetComponent<Info>().Hp);
                }
                if (time >= 7f) // SkillCooldown 쓴직후 예를들어 32초에 사용하면 36초에 다시 사용가능함.. 2,3,4,5,6 의 5초방식
                {
                    this.gameObject.GetComponent<Info>().skiil1 = 1;
                    //Debug.Log(this.gameObject.name + "10초가 지났습니다");
                    time = 0;
                }
                target.gameObject.GetComponent<Info>().Hp -= damage;
                //Debug.Log(this.gameObject.name + " 이 공격하여 " + target.gameObject.name + "의 피가  " + target.gameObject.GetComponent<Info>().Hp);
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        refEnemyDanger.Remove(refEnemyDanger[refEnemyList.IndexOf(target.gameObject)]);
                        refEnemyList.Remove(target.gameObject);
                        target.GetComponent<Ai>().Die();
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                    if (target != null)
                        vector = (target.position - transform.position).normalized;
                }

                yield return new WaitForSeconds(7f);
                isAttack = true;          
        }
    }

    IEnumerator Co_Attack()
    {
        float damage = 0;
        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange)
        {
                isAttack = false;
                animator.SetTrigger("Attack");

                if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
                {
                    if (Random.Range(1, 1000) < info.Critcal * 10)
                    {

                        damage = (int)(info.Power * info.CD * Random.Range(0.98f, 1.02f)) - target.GetComponent<Info>().PhyArmor;
                        Debug.Log("Critical!!");
                    }
                    else
                    {
                        damage = (int)(info.Power * Random.Range(0.98f, 1.02f)) - target.GetComponent<Info>().PhyArmor;
                    }
                    target.gameObject.GetComponent<Info>().Hp -= damage;
                    //Debug.Log(this.gameObject.name + " 이 공격하여 " + target.gameObject.name + "의 피가  " + target.gameObject.GetComponent<Info>().Hp);
                }
                else
                {
                    Debug.Log("적 회피!");
                }
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        refEnemyDanger.Remove(refEnemyDanger[refEnemyList.IndexOf(target.gameObject)]);
                        refEnemyList.Remove(target.gameObject);
                    target.GetComponent<Ai>().Die();
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

    /*IEnumerator job_skill1()
    {
        float damage = 0;
        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange)
        {
            if (info.AttackRange < 5.0f)
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
                {
                    if (Random.Range(1, 1000) < info.Critcal * 10)
                    {
                        damage = info.Power * info.CD - target.GetComponent<Info>().Armor;
                    }
                    else
                    {
                        damage = info.Power - target.GetComponent<Info>().Armor;
                    }

                    target.gameObject.GetComponent<Info>().Hp -= damage;

                    Debug.Log(target.gameObject.name + " / " + target.gameObject.GetComponent<Info>().Hp);
                }
                else
                {
                    Debug.Log("적 회피!");
                }
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
            else
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                GameObject arrowPrefeb = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, -90f));
                arrowPrefeb.transform.parent = gameObject.transform;

                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
        }

        yield return new WaitForSeconds(5f);
    }

    IEnumerator job_skill2()
    {
        float damage = 0;
        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange)
        {
            if (info.AttackRange < 5.0f)
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
                {
                    if (Random.Range(1, 1000) < info.Critcal * 10)
                    {
                        damage = info.Power * info.CD - target.GetComponent<Info>().Armor;
                    }
                    else
                    {
                        damage = info.Power - target.GetComponent<Info>().Armor;
                    }

                    target.gameObject.GetComponent<Info>().Hp -= damage;

                    Debug.Log(target.gameObject.name + " / " + target.gameObject.GetComponent<Info>().Hp);
                }
                else
                {
                    Debug.Log("적 회피!");
                }
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
            else
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                GameObject arrowPrefeb = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, -90f));
                arrowPrefeb.transform.parent = gameObject.transform;

                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
        }
        yield return new WaitForSeconds(5f);
    }

    IEnumerator job_skill3()
    {
        float damage = 0;
        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange)
        {
            if (info.AttackRange < 5.0f)
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
                {
                    if (Random.Range(1, 1000) < info.Critcal * 10)
                    {
                        damage = info.Power * info.CD - target.GetComponent<Info>().Armor;
                    }
                    else
                    {
                        damage = info.Power - target.GetComponent<Info>().Armor;
                    }

                    target.gameObject.GetComponent<Info>().Hp -= damage;

                    Debug.Log(target.gameObject.name + " / " + target.gameObject.GetComponent<Info>().Hp);
                }
                else
                {
                    Debug.Log("적 회피!");
                }
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
            else
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                GameObject arrowPrefeb = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, -90f));
                arrowPrefeb.transform.parent = gameObject.transform;

                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
        }
        yield return new WaitForSeconds(5f);
    }

    IEnumerator job_skill4()
    {
        float damage = 0;
        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange)
        {
            if (info.AttackRange < 5.0f)
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
                {
                    if (Random.Range(1, 1000) < info.Critcal * 10)
                    {
                        damage = info.Power * info.CD - target.GetComponent<Info>().Armor;
                    }
                    else
                    {
                        damage = info.Power - target.GetComponent<Info>().Armor;
                    }

                    target.gameObject.GetComponent<Info>().Hp -= damage;

                    Debug.Log(target.gameObject.name + " / " + target.gameObject.GetComponent<Info>().Hp);
                }
                else
                {
                    Debug.Log("적 회피!");
                }
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
            else
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                GameObject arrowPrefeb = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, -90f));
                arrowPrefeb.transform.parent = gameObject.transform;

                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
        }
        yield return new WaitForSeconds(5f);
    }

    IEnumerator job_skill5()
    {
        float damage = 0;
        if (Vector2.Distance(transform.position, target.position) <= info.AttackRange)
        {
            if (info.AttackRange < 5.0f)
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                if (Random.Range(1, 1000) > target.GetComponent<Info>().Evade * 10)
                {
                    if (Random.Range(1, 1000) < info.Critcal * 10)
                    {
                        damage = info.Power * info.CD - target.GetComponent<Info>().Armor;
                    }
                    else
                    {
                        damage = info.Power - target.GetComponent<Info>().Armor;
                    }

                    target.gameObject.GetComponent<Info>().Hp -= damage;

                    Debug.Log(target.gameObject.name + " / " + target.gameObject.GetComponent<Info>().Hp);
                }
                else
                {
                    Debug.Log("적 회피!");
                }
                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
            else
            {
                isAttack = false;
                animator.SetTrigger(jobName + "Attack");

                GameObject arrowPrefeb = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, -90f));
                arrowPrefeb.transform.parent = gameObject.transform;

                if (target.gameObject.GetComponent<Info>().Hp <= 0 || !target.gameObject.activeSelf)
                {
                    //enemyDanger.Clear();
                    //enemyList.Clear();
                    if (target.gameObject.activeSelf)
                    {
                        enemyDanger.Remove(enemyDanger[enemyList.IndexOf(target.gameObject)]);
                        enemyList.Remove(target.gameObject);
                        target.gameObject.SetActive(false);
                    }
                    //enemyDanger.RemoveAt(location);
                    target = null;
                    SetDanger();
                    Settarget();
                }

                yield return new WaitForSeconds(info.AttackSpeed);
                isAttack = true;
            }
        }
        yield return new WaitForSeconds(5f);
    }*/

    IEnumerator fire()
    {
        if ((bool)_state[(int)State.Fire])
            info.Hp -= 100;
        isAttack = false;
        yield return new WaitForSeconds(5f);
        isAttack = true;
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

    public void Move()
    {
        if (target == null)
            return;

        animator.SetTrigger("Move");

        vector = (target.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, target.position) > info.AttackRange)
            transform.position += new Vector3(vector.x / Mathf.Abs(vector.x), vector.y / Mathf.Abs(vector.x), -vector.y / Mathf.Abs(vector.x) * 10) * 3f *info.MoveSpeed * Time.deltaTime;
    }

    public int Selectmin(int mindistance, int newdistance)
    {
        if (Vector2.Distance(transform.position, refEnemyList[mindistance].transform.position) > Vector2.Distance(transform.position, refEnemyList[newdistance].transform.position))
            mindistance = newdistance;

        return mindistance;
    }

    public int SelectDangerMax(int MaxDanger, int newDanger)
    {
        if (refEnemyDanger[MaxDanger] < refEnemyDanger[newDanger])
            MaxDanger = newDanger;
        else if (refEnemyDanger[MaxDanger] == refEnemyDanger[newDanger] && MaxDanger != newDanger)
            return -1;

        return MaxDanger;
    }

    public Transform Settarget()
    {
        if (refEnemy.Length == 0)
        {
            //Reload_Ai();
            return null;
        }
        if (refEnemyList.Count == 0)
        {
            foreach (GameObject _enemy in refEnemy)
            {
                //지상 공중 구분 넣을곳
                refEnemyList.Add(_enemy);
                refEnemyDanger.Add(_enemy.GetComponent<Info>().Danger);
            }
        }
        if (refEnemy != null)
        {
            if (refEnemyList.Count == 1)
            {
                target = refEnemyList[0].transform;
            }
            else
            {
                min = 0;
                max = 0;
                for (int i = 0; refEnemyList.Count > i; i++)
                {

                    if (SelectDangerMax(max, i) == -1)
                    {
                        target = refEnemyList[Selectmin(min, i)].transform;
                        //Debug.Log(enemyDanger[SelectDangerMax(max, i)]);
                        //location = Selectmin(min, i);
                    }
                    else
                    {
                        target = refEnemyList[SelectDangerMax(max, i)].transform;
                        //Debug.Log(enemyDanger[SelectDangerMax(max, i)]);
                        //location = SelectDangerMax(max, i);
                    }
                }
            }
            vector = new Vector3((target.position.x - transform.position.x) / Mathf.Abs(target.position.x - transform.position.x),
               (target.position.y - transform.position.y) / Mathf.Abs(target.position.y - transform.position.y), 0f);
        }

        if (target != null)
            vector = target.position - transform.position - new Vector3(0.5f, 0f, 0f);

        //Reload_Ai();
        return target;
    }

    public void SetDanger()
    {
        refEnemy = GameObject.FindGameObjectsWithTag(Enemy_tag);
        count = refEnemy.Length;
        GameManager.instance.SetCount(count, Enemy_tag);

        if (refEnemy.Length == 0)
        {
            count = 0;
            GameManager.instance.SetCount(count, Enemy_tag);
            return;
        }

        if (refEnemyList.Count == 0)
        {
            foreach (GameObject _enemy in refEnemy)
            {
                refEnemyList.Add(_enemy);
                refEnemyDanger.Add(_enemy.GetComponent<Info>().Danger);
            }
        }

        int i = 0;
        foreach (GameObject _enemy in refEnemy)
        {
            refEnemyDanger[i] -= (int)(Vector3.Distance(transform.position, _enemy.transform.position));
            i++;
        } 
    }

    public void setFlipX()
    {
        if (target == null)
            return;
        if (target.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

}
