using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public string Name;
    public float MaxHp;
    public float Hp;
    public float AttackDistance; // 공격 거리
    public float AttackDelay;
    public float ApplyAttackDelay;
    public float CurrentAttackDelay;
    public float Power;

    public Animator anim;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        CurrentAttackDelay = AttackDelay;
        ApplyAttackDelay = AttackDelay;
    }

    public virtual void Update()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
        CurrentAttackDelay += Time.deltaTime;
        animUpdate();
    }

    public virtual void FixedUpdate()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
    }



    public virtual void DieCheck()
    {
        if(Hp <= 0)
        {
            anim.SetBool("isDie",true);
        }
    }

    public virtual void Damaged(float value)
    {
        anim.SetTrigger("Damaged");
        Hp -= value;
    }


    public abstract void animUpdate();
    
}
