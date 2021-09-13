﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinViliageBoss : BossMonster
{

    [SerializeField]
    GameObject spawnCircle;

    float skillCool;
    public override void Awake()
    {
        base.Awake();
        Hp = applyMaxHp;
    }

    public override void Start()
    {
        base.Start();
        skillCool = Time.time + 3;
    }

    public override void Update()   
    {
        base.Update();
        passive_ATK_increase();
        if (Time.time >= skillCool)
        {
            skillCool = Time.time + 6;

            int skillIndex = Random.Range(0, 2);
            anim.SetInteger("SkillIndex", skillIndex);
            switch (skillIndex)
            {
                case 0:
                    Skill_Smash();
                    break;
                case 1:
                    Skill_spawnGoblin();
                    break;

                default:
                    break;
            }
        }
    }


    public override void OnEnable()
    {
        applyPower = Power;
    }
    public void passive_ATK_increase()
    {
        float per = 100 - ((float)Hp / MaxHp) *100;
        applyPower = Power + per / 2;
    }

    public override void Attack()
    {
        BattleManager.Instance.PlayerDamage(applyPower);
    }

    public void Skill_spawnGoblin()
    {
        anim.SetTrigger("Skill");
       
        
    }

    public void spawnGoblin()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 3);

            switch (index)
            {
                case 0:
                    Instantiate(spawnCircle, new Vector2(transform.position.x + Random.Range(1, 3), 0.51f), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(spawnCircle, new Vector2(transform.position.x + Random.Range(1, 3), 0.35f), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(spawnCircle, new Vector2(transform.position.x + Random.Range(1, 3), 0.123f), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }

    public void Skill_Smash() {
        anim.SetInteger("SkillIndex",0);
        anim.SetTrigger("Skill");
    }
}
