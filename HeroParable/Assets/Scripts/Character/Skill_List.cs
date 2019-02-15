using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum Skill_Name { Nomal_Attack, Strong_Attack, Critical_Attack, Weak_Attack };

enum Attribute { Fire,Water,Wind };

enum State { Nomal, Stun, Silence, Taunt, Fire, NumberofType };

enum Attack_type { Active , Passive };
enum Skill_Ready_type { Channeling , Instant }

namespace Skill_System
{
    public class Skill : MonoBehaviour
    {
        protected int level;
        protected int target_num;
        protected float damage;
        protected float cooltime;
        protected float range;
        protected float percent;
        protected float duration;
        protected List<bool> cc;

        protected List<bool> Attack;
        protected List<bool> Skill_Ready;

        public Skill()
        {
            level = 0;
            target_num = 0;
            damage = 0f;
            cooltime = 0f;
            range = 0f;
            percent = 0f;
            duration = 0f;

            cc = new List<bool>();
            Attack = new List<bool>();
            Skill_Ready = new List<bool>();

            cc.Add(true);
            for (int i = 1; i < (int)State.NumberofType; i++)
            {
                cc.Add(false);
            }

            for (int i = 0; i < 2; i++)
            {
                Attack.Add(false);
            }

            for (int i = 0; i < 2; i++)
            {
                Skill_Ready.Add(false);
            }
        }

        public int GetLevel()
        {
            return level;
        }
        public int Gettarget_num()
        {
            return target_num;
        }

        public float Getdamage()
        {
            return damage;
        }
        public float Getcooltime()
        {
            return cooltime;
        }
        public float Getrange()
        {
            return range;
        }
        public float Getpercent()
        {
            return percent;
        }
        public float Getduration()
        {
            return duration;
        }

        public void SetLevel(int level)
        {
            this.level = level;
        }
        public void Settarget_num(int target_num)
        {
            this.target_num = target_num;
        }
        public void Setdamage(float damage)
        {
            this.damage = damage;
        }
        public void Setcooltime(float cooltime)
        {
            this.cooltime = cooltime;
        }
        public void Setrange(float range)
        {
            this.range = range;
        }
        public void Setpercent(float percent)
        {
            this.percent = percent;
        }
        public void Setduration(float duration)
        {
            this.duration = duration;
        }
    }

    public class Units_Skill : Skill 
    {
        Ai unit;

        public void Set_Skill_Stat(int _level, int _target_num, float _damage, float _cooltime, float _range, float _percent, float _duration, int[] _cc, int _attack, int _skillready)
        {
            level = _level;
            target_num = _target_num;
            damage = _damage;
            cooltime = _cooltime;
            range = _range;
            percent = _percent;
            duration = _duration;

            for (int i = 0; i < _cc.Length; i++)
            {
                cc[_cc[i]] = true;
            }
            
            Attack[_attack] = true;
            Skill_Ready[_skillready] = true;
        }

        public void Reload_Ai(Ai _unit)
        {
            unit = _unit;
        }

        //IEnumerator Nomal_Attack()
        //{
        //    float damage = 0;
        //    if (Vector2.Distance(unit.transform.position, unit.target.position) <= unit.info.AttackRange)
        //    {
        //        if (unit.info.AttackRange < 5.0f)
        //        {
        //            unit.isAttack = false;
        //            unit.animator.SetTrigger("Attack");

        //            if (UnityEngine.Random.Range(1, 1000) > unit.target.GetComponent<Info>().Evade * 10)
        //            {
        //                if (UnityEngine.Random.Range(1, 1000) < unit.info.Critcal * 10)
        //                {
        //                    damage = (int)(unit.info.Power * unit.info.CD * UnityEngine.Random.Range(0.98f, 1.02f)) - unit.target.GetComponent<Info>().PhyArmor;
        //                    Debug.Log("Critical!!");
        //                }
        //                else
        //                {
        //                    damage = (int)(unit.info.Power * UnityEngine.Random.Range(0.98f, 1.02f)) - unit.target.GetComponent<Info>().PhyArmor;
        //                }

        //                unit.target.gameObject.GetComponent<Info>().Hp -= damage;

        //                Debug.Log(unit.target.gameObject.name + " / " + unit.target.gameObject.GetComponent<Info>().Hp);
        //            }
        //            else
        //            {
        //                Debug.Log("적 회피!");
        //            }
        //            if (unit.target.gameObject.GetComponent<Info>().Hp <= 0 || !unit.target.gameObject.activeSelf)
        //            {
        //                //enemyDanger.Clear();
        //                //enemyList.Clear();
        //                if (unit.target.gameObject.activeSelf)
        //                {
        //                    unit.refEnemyDanger.Remove(unit.refEnemyDanger[unit.refEnemyList.IndexOf(unit.target.gameObject)]);
        //                    unit.refEnemyList.Remove(unit.target.gameObject);
        //                    unit.target.gameObject.SetActive(false);
        //                }
        //                //enemyDanger.RemoveAt(location);
        //                unit.target = null;
        //                unit.SetDanger();
        //                unit.Settarget();
        //                if (unit.target != null)
        //                    unit.vector = (unit.target.position - unit.transform.position).normalized;
        //            }

        //            yield return new WaitForSeconds(unit.info.AttackSpeed);
        //            unit.isAttack = true;
        //        }
        //        else
        //        {
        //            unit.isAttack = false;
        //            unit.animator.SetTrigger("Attack");

        //            GameObject arrowPrefeb = Instantiate(unit.arrow, unit.transform.position, Quaternion.Euler(0f, 0f, -90f));
        //            arrowPrefeb.transform.parent = unit.gameObject.transform;

        //            if (unit.target.gameObject.GetComponent<Info>().Hp <= 0 || !unit.target.gameObject.activeSelf)
        //            {
        //                //enemyDanger.Clear();
        //                //enemyList.Clear();
        //                if (unit.target.gameObject.activeSelf)
        //                {
        //                    unit.refEnemyDanger.Remove(unit.refEnemyDanger[unit.refEnemyList.IndexOf(unit.target.gameObject)]);
        //                    unit.refEnemyList.Remove(unit.target.gameObject);
        //                    unit.target.gameObject.SetActive(false);
        //                }
        //                //enemyDanger.RemoveAt(location);
        //                unit.target = null;
        //                unit.SetDanger();
        //                unit.Settarget();
        //            }

        //            yield return new WaitForSeconds(unit.info.AttackSpeed);
        //            unit.isAttack = true;
        //        }
        //    }

        //}

        public void Using_Skill(int num)
        {
            switch (num)
            {
                case (int)Skill_Name.Nomal_Attack:
                    Debug.Log("skill 1");
                    //StartCoroutine(Nomal_Attack());
                    break;
            }
        }
    }
}