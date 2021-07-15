using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public Transform originPos; //원래 위치(돌진)
    Character character;
    public Transform DashTransform; // 돌진으로 가야하는 위치(돌진)
    int FirstTargetcount = 0; // 첫번째 공격상대인지 판별하기위한 변수
    Collider2D target;// 공격타겟
    KnightAnimatorController animController;

    bool isDash = false;
    void Awake()
    {
        character = GetComponent<Character>();
        animController = GetComponent<KnightAnimatorController>();
    }

    void Update()
    {
        DieCheck();
        Skill_Dash();
    }

    private void FixedUpdate()
    {

        ConcactCheck();
    }





    void ATK() // 공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(character.attackTransform.position, character.AttackRange, LayerMask.GetMask("Monster"));
        
        foreach (Collider2D enemy in hitEnemy)
        {
            if(target == null)
            {
                FirstTargetcount = 0;
            }
            if (FirstTargetcount == 0)
            {
                
                target = enemy;
            }
            FirstTargetcount++;
            target.GetComponent<Monster>().Monster_Damage(character.StrikingPower);
            break;
        }
    }

    void ConcactCheck() // 배틀상태 감지
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, 0.5f);

        if (hitinfo.collider != null && isDash == false)
        {
            BattleManager.Instance.isContactP = true;
            hitinfo.collider.GetComponent<Monster>().Stop = true;
        }
        else
        {
            BattleManager.Instance.isContactP = false;
            GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");

            foreach (GameObject monster in Monsters)
            {
                monster.GetComponent<Monster>().Stop = false;
            }
        }
    }

    private void OnDrawGizmosSelected()//기즈모그리기
    {

        
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

    public void Skill_Dash()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Dash");
            StartCoroutine(DashCroutine());
            
        }
    }


    IEnumerator DashCroutine()
    {
        animController.SetDash(true);
        BattleManager.Instance.isDash = true;
        isDash = true;
        while (transform.position.x < DashTransform.position.x-0.2f)
        {
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(character.attackTransform.position, character.AttackRange, LayerMask.GetMask("Monster"));
            BattleManager.Instance.isDash = true;
            foreach (Collider2D enemy in hitEnemy)
            {
                enemy.transform.position = new Vector2(transform.position.x + 0.6f, enemy.transform.position.y);
            }
            transform.position = Vector2.Lerp(transform.position, DashTransform.position, 0.02f);

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        
        while (transform.position.x > originPos.transform.position.x + 0.1f)
        {
            
            transform.position = Vector2.Lerp(transform.position, originPos.transform.position, 0.08f);
            yield return null;
            
        }
        
        
        BattleManager.Instance.isDash = false;
        transform.position = originPos.position;
        animController.SetDash(false);
        isDash = false;
        StopAllCoroutines();
    }
}
