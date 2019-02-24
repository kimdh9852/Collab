using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum Skill_Name { Nomal_Attack, Strong_Attack, Critical_Attack, Weak_Attack };

enum Attribute { Fire, Water, Wind };

enum State { Nomal, Stun, Silence, Taunt, Poison, NumberofType };

enum Attack_type { Active, Passive };

enum Target_type { Self, Enemy, Team };

enum Skill_Ready_type { Channeling, Instant }


namespace Skill_System
{
    public class Skill : MonoBehaviour
    {

        Ai ai;

        public class Skill_struct
        {
            public string skill_name;
            public int level;
            public int release_level;
            public int target_num;
            public int target_type;
            public float Sum_damage;
            public float Per_damage;
            public float cooltime;
            public float range;
            public float percent;
            public float duration;
            public bool buff;
            public int hit;
            public float recovery;
            public int Danger;
            public float Damage_reduction;
            public float PhyArmor;
            public float MagicalArmor;
            public float AttackRange;
            public float AttackSpeed;
            public float MoveSpeed;
            public float Critcal;
            public float Evade;
            public float CD;
            public float Accuracy;

            public List<bool> cc;
            public List<int> skill_ready_type;
        }

        static public Skill_struct skell_big = new Skill_struct();

        public void Awake()
        {
            ai = GetComponent<Ai>();
            skell_big = new Skill_struct();
            skell_big.skill_name = "강타";
            skell_big.level = 1;
            skell_big.release_level = 1;
            skell_big.target_num = 1;
            skell_big.target_type = 0;
            skell_big.Sum_damage = 0;
            skell_big.cooltime = 0;
            skell_big.range = 0;
            skell_big.percent = 0;
            skell_big.duration = 0;
            skell_big.buff = true;
        }

        void Passive_buf(Skill_struct info)
        {

            ai.info.Power += (info.Sum_damage + info.Per_damage * 0.01f);
            //Danger = info.Danger;
            //ai.info.Damage_reduction = info.Damage_reduction;
            ai.info.PhyArmor = info.PhyArmor;
            ai.info.MagicalArmor = info.MagicalArmor;
            ai.info.AttackRange = info.AttackRange;
            ai.info.AttackSpeed = info.AttackSpeed;
            ai.info.MoveSpeed = info.MoveSpeed;
            ai.info.Critcal = info.Critcal;
            ai.info.Evade = info.Evade;
            ai.info.CD = info.CD;
            ai.info.Accuracy = info.Accuracy;

        }

        IEnumerator Poison()
        {
            if ((bool)ai._state[(int)State.Poison])
                ai.info.Hp -= ai.info.Power * 0.01f;
            yield return new WaitForSeconds(1f);
        }

        public void ActiveSkill()
        { }

        // 타겟 지정 (수, 타입)
        //public void Set_Target(Target_typㄷ,Targetnumber)
        //{

        //}

        public void skill1(bool exist, string name, int level)
        {
            if (!exist)
                return;

            if (ai.info.Attack_type == (int)Attack_type.Passive)
            {
                //타겟 넘...
                //공격이라 버프 두가지로 구분 
                //Passive_buf(ai.info);
            }
            else if (ai.info.Attack_type == (int)Attack_type.Active)
            {
                ActiveSkill();
            }
            //패시브 or 액티브/ 버프 or 공격/
        }

        public void skill2(bool exist, string name, int level)
        {

        }

        // 전이 스킬 
        public void metastasis()
        {

        }
    }

    // 특수한 스킬(전이, 독뎀 , 힐 등)따로 만들고 기본적인 틀은 SKill1안에서 코딩 특수한 스킬은 Skillname에서 따로구분해서 실행하게 
    // 1. DB에서 스킬 이름을 불러와서 비교하고 같으면 가져오고 
    // 2. Skill타입구분 특수스킬이면 사용 
    // 3. Skill1 실행 

}