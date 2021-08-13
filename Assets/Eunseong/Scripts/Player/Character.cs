using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : Actor
{
    /* public string characterName;
     public float MaxHp;
     [HideInInspector]
     public float Hp;

     
     [Range(1f,5f)]
     public float AttackDistance; // 공격 거리
     public float AttackDelay;
     public float StrikingPower;


     Animator anim;*/

    public float MaxMp;

    [HideInInspector]
    public float Mp;

    int Mpcnt;
    public bool isAttack;
    public override void Start()
    {
        base.Start();
        Hp = MaxHp;
        Mp = MaxMp;
    }

    public override void Update()
    {
        base.Update();
        animUpdate();
        Attack();
        DieCheck();
    }

    public virtual void FixedUpdate()
    {
        AttackStateCheck();
        ConcactCheck();

    }
    public override void Damaged(float value) // 데미지 입는함수
    {
        if (Hp > 0)
        {
            if (!BattleManager.Instance.isUseSkill)
            {
                anim.SetTrigger("Damaged");
            }
            Hp -= value;
        }
    }

    public bool UseMpCheck(float value) //마나 깎는 함수
    {
        if (Mp - value >= 0)
        {
            
            return true;
        }
        else
        {
            GameSceneUIManager.Instance.ApearWarningText(1);
            return false;
        }

    }


    public override void DieCheck() //죽는거 체크
    {
        if (Hp <= 0)
        {
            BattleManager.Instance.isEnd = true;
            anim.SetBool("isDie", true);
            return;
        }
    }


    public virtual IEnumerator HpMpRecovery(float second, float Hpvalue, float Mpvalue)
    {
        while (!BattleManager.Instance.isEnd)
        {
            yield return new WaitForSeconds(second);
            if (Hp < MaxHp)
            {
                Hp += Hpvalue;
            }
            if (Mp < MaxMp)
            {
                Mp += Mpvalue;
            }
        }
    }
    public override void animUpdate()
    {
        anim.SetBool("isContact", BattleManager.Instance.isContact);
    }

    
public virtual void SetisUseSkillFalse()
    {
        BattleManager.Instance.isUseSkill = false;
    }

    public virtual void ConcactCheck() // 맞닿음 감지
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, 1, LayerMask.GetMask("Monster"));

        BattleManager.Instance.isContact = (hitinfo.collider != null) ? true : false;

    }

    public virtual void AttackStateCheck() // 공격가능 상태 판별
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, AttackDistance, LayerMask.GetMask("Monster"));

        isAttack = (hitinfo.collider != null) ? true : false;

    }

    

    public virtual void Attack()
    {

        if (isAttack)
        {


            if (!BattleManager.Instance.isUseSkill)
            {

                if (CurrentAttackDelay >= AttackDelay)
                {
                    anim.SetTrigger("Attack");

                    CurrentAttackDelay = 0;
                }
            }
        }
    }

    public bool UseSkill(float CoolTimeCheck, float MPCheck , List<int> list)
    {



        if (BattleManager.Instance.PatternInputEnd)
        {

            if (!BattleManager.Instance.isUseSkill)
            {


                if (UseMpCheck(MPCheck))
                {
                    

                    if (BattleManager.Instance.Pattern_id.SequenceEqual(list))
                    {
                        if (CoolTimeCheck <= 0)
                        {
                            return true;
                        }
                        else
                        {
                            GameSceneUIManager.Instance.ApearWarningText(2);
                        }
                    }
                }
            }

        }

        return false;

    }
}
