using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : Character
{
    
    public Transform AttackTransform; //공격 위치
    public float AttackRange; // 공걱 범위


    int FirstTargetcount = 0; // 첫번째 공격상대인지 판별하기위한 변수
    Collider2D target;// 공격타겟

    [HideInInspector]
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void SingleATK() // 단일공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackTransform.position, AttackRange, LayerMask.GetMask("Monster"));

        foreach (Collider2D enemy in hitEnemy)
        {


            if (FirstTargetcount == 0)
            {

                target = enemy;
            }

            FirstTargetcount++;

            if (target.gameObject.activeSelf == false || target.gameObject == null)
            {
                FirstTargetcount = 0;
                target = enemy;
            }
    

            target.GetComponent<Monster>().Damaged(Power);
            break;
        }
    }
    public void MultiATK() // 다중공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackTransform.position, AttackRange, LayerMask.GetMask("Monster"));

        foreach (Collider2D enemy in hitEnemy)
        {

            enemy.GetComponent<Monster>().Damaged(ATKDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackTransform.position, AttackRange);
        Gizmos.DrawWireCube(AllAttackPosition.position, AllAttackRange);

    }

}
