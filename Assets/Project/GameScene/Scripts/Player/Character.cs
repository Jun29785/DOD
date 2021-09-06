    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DOD.DB;
using DOD.Define;

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
    
    public bool isAttack;

    public Slider hpSlider;
    public Slider mpSlider;
    public TextMeshProUGUI HpText;

    public float ATKDamage;

    public Transform AllAttackPosition;

    public Vector2 AllAttackRange;
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
        hp_mp_UIUpdate();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AttackStateCheck();

    }

    public override void SetData(int Key)
    {
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

    public void UseMp(float value)
    {
        Mp -= value;
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

    

    public virtual void AttackStateCheck() // 공격가능 상태 판별
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, AttackDistance, LayerMask.GetMask("Monster"));

        isAttack = (hitinfo.collider != null && !BattleManager.Instance.isUseSkill) ? true : false;
        BattleManager.Instance.isContact = (hitinfo.collider != null &&!BattleManager.Instance.isUseSkill) ? true : false;

    }

    public virtual void hp_mp_UIUpdate()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, Hp/MaxHp, 8f * Time.deltaTime);
        mpSlider.value = Mathf.Lerp(mpSlider.value, Mp / MaxMp, 8f * Time.deltaTime);
        HpText.text = Hp.ToString() + " / " + MaxHp.ToString();

    }

    public virtual void Attack()
    {

        if (isAttack)
        {


            if (!BattleManager.Instance.isUseSkill)
            {

                if (CurrentAttackDelay >= ApplyAttackDelay && !BattleManager.Instance.isDash)
                {
                    anim.SetTrigger("Attack");

                    CurrentAttackDelay = 0;
                }
            }
        }
    }
    /// <summary>
    /// 스킬 사용 함수
    /// </summary>
    public bool UseSkill(skillEnum SE,List<int> list, float CoolTimeCheck)
    {



        if (BattleManager.Instance.PatternInputEnd)
        {

            if (!BattleManager.Instance.isUseSkill)
            {


                if (UseMpCheck(DataBaseManager.Instance.tdSkillDict[(int)SE].Fmana))
                {


                    if (BattleManager.Instance.Pattern_id.SequenceEqual(DataBaseManager.Instance.tdSkillDict[(int)SE].Command))
                    {
                        if (CoolTimeCheck <= 0)
                        {
                            GameSceneUIManager.Instance.Create_SkillCoolTimeObject(DataBaseManager.Instance.tdSkillDict[(int)SE].Name, DataBaseManager.Instance.tdSkillDict[(int)SE].Ctime);
                            return true;
                        }
                        else
                        {
                            GameSceneUIManager.Instance.ApearWarningText(2);
                        }
                    }
                }
            }
            /*else
            {
                GameSceneUIManager.Instance.ApearWarningText(3);
            }*/

        }
        

        return false;

    }


}
