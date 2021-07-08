using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float MaxHp = 5;
    float Hp;
    public Transform attackTransform; //공격 위치
    public float AttackRange; // 공걱 범위

    #region 프로퍼티
    public float MaxHpP
    {
        get
        {
            return MaxHp;
        }
        set
        {
            MaxHp = value;
        }
    }
    public float HpP
    {
        get
        {
            return Hp;
        }
        set
        {
            Hp = value;
        }
    }

    #endregion
    void Start()
    {
        Hp = MaxHp;
    }

    void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x,transform.position.y + 0.5f), Vector2.right);
        DieCheck(); 
    }

    private void FixedUpdate()
    {

        BattleCheck();
    }

    public void Damage(float value) // 데미지 입는함수
    {
        Hp -= value;
        Debug.Log(this.Hp);
    }


    void ATK() // 공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackTransform.position, AttackRange, LayerMask.GetMask("Monster"   ));

        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log($"hit {enemy.name}");
            enemy.GetComponent<Monster>().Monster_Damage(1);
            
        }
    }

    void BattleCheck() // 배틀상태 감지
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, 0.5f);

        if (hitinfo.collider != null)
        {
            BattleManager.instance.isBattleP = true;
        }
        else
        {
            BattleManager.instance.isBattleP = false;
        }
    }

    private void OnDrawGizmosSelected()
    {

        if (attackTransform == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackTransform.position, AttackRange);
    }

    public void DieCheck()
    {
        if(Hp <= 0)
        {
            Debug.Log("Die!");
            return;
        }
    }
}
