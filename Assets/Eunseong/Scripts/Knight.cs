using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    Character character;



    void Awake()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        DieCheck(); 
    }

    private void FixedUpdate()
    {

        BattleCheck();
    }





    void ATK() // 공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(character.attackTransform.position, character.AttackRange, LayerMask.GetMask("Monster"   ));

        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log($"hit {enemy.name}");
            enemy.GetComponent<Monster>().Monster_Damage(character.StrikingPower);
            
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

    private void OnDrawGizmosSelected()//기즈모그리기
    {

        if (character.attackTransform == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(character.attackTransform.position, character.AttackRange);
    }

    public void DieCheck() //죽는거 체크
    {
        if(character.Hp <= 0)
        {
            Debug.Log("Die!");
            return;
        }
    }
}
