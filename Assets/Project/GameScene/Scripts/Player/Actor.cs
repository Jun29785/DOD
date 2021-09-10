using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.DB;
public abstract class Actor : MonoBehaviour
{
    [Header("State")]
    public int unitNo; //유닛 넘버
    public string Name; // 유닛 이름
    public float MaxHp; // 최대 체력
    [HideInInspector]
    public float applyMaxHp; // 현재 적용된 최대 체력
    public float Hp; // 현재 체력
    public float AttackDistance; // 공격 거리
    public float AttackDelay; // 공격속도
    [HideInInspector]
    public float ApplyAttackDelay; // 현재 적용된 공격속도
    [HideInInspector]
    public float CurrentAttackDelay; 
    public float Power; // 공격력
    [HideInInspector]
    public float applyPower; // 현재 적용된 공격력

    public Animator anim;

    public virtual void Awake()
    {

        CurrentAttackDelay = AttackDelay;
        ApplyAttackDelay = AttackDelay;
        applyPower = Power;
        applyMaxHp = MaxHp;
    }


    public abstract void SetData(int Key); // 유닛 데이터 세팅
    public virtual void Start()
    {
        anim = GetComponent<Animator>();

    }

    public virtual void Update()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }

        if(Hp > MaxHp)
        {
            Hp = MaxHp;
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
