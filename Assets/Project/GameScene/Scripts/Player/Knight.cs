using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DOD.DB;
using DOD.Define;

public class Knight : MeleeCharacter
{

    public Transform originPos; //원래 위치(돌진)
    public Transform DashTransform; // 돌진으로 가야하는 위치(돌진)

    Dictionary<string, float> currentSkillcoolTimeDic = new Dictionary<string, float>();
    Dictionary<string, float> SkillcoolTimeDic = new Dictionary<string, float>();

    public override void Awake()
    {


    }
    public override void Start()
    {

        base.Start();

        foreach (var item in DataBaseManager.Instance.tdSkillDict.Values)
        {
            SkillcoolTimeDic.Add(item.Name, item.Ctime);
            currentSkillcoolTimeDic.Add(item.Name, 0);
        }

        currentSkillcoolTimeDic.Add("BigSword", 0);

        SkillcoolTimeDic.Add("BigSword", 8); StartCoroutine(HpMpRecovery(1.5f, 1, 3));
    }
    public override void Update()
    {
        base.Update();
        print(BattleManager.Instance.currentmap);
        Skill_Dash();
        Skill_Sting();
        Skill_SpinAttack();
        Skill_BigSword();
    }

    #region 판정
    public void BigSwordATK() // 검이커져! 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(AllAttackPosition.position, AllAttackRange, 0, LayerMask.GetMask("Monster"));

        foreach (Collider2D enemy in hitEnemy)
        {
            enemy.GetComponent<Monster>().Damaged(ATKDamage);
        }
    }

    #endregion
    #region 스킬

    #region 돌진

    /// <summary>
    /// 돌진
    /// </summary>
    public void Skill_Dash()
    {

        currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Name] -= Time.deltaTime;


        if (UseSkill(DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Name,
            DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Ctime,
            currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Name/* 돌진 */],
            DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Fmana,
            DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Command))
        {
            ATKDamage = DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Fdmg;
            UseMp(DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Fmana);  
            StartCoroutine(DashCroutine());
        }








    }
    /// <summary>
    /// 돌진 판정
    /// </summary>
    IEnumerator DashCroutine()
    {
        currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Name] = SkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Name];

        float currentTime = 1;

        BattleManager.Instance.isUseSkill = true;
        BattleManager.Instance.isDash = true;


        anim.SetBool("isDash", true);
        while (transform.position.x < DashTransform.position.x - 0.2f)
        {
            currentTime += Time.deltaTime;
            BattleManager.Instance.isUseSkill = true;
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackTransform.position, 0.2f, LayerMask.GetMask("Monster"));
            foreach (Collider2D enemy in hitEnemy)
            {
                enemy.transform.position = new Vector2(transform.position.x + 0.6f, enemy.transform.position.y);
                if (currentTime >= 0.4f)
                {
                    enemy.GetComponent<Monster>().Damaged(5);
                }

            }

            transform.position = Vector2.Lerp(transform.position, DashTransform.position, 2.2f * Time.deltaTime);
            if (currentTime >= 0.4f)
            {

                currentTime = 0;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("isDash", false);

        while (transform.position.x > originPos.transform.position.x + 0.1f)
        {

            transform.position = Vector2.Lerp(transform.position, originPos.transform.position, 6 * Time.deltaTime);
            yield return null;
        }


        transform.position = originPos.position;


        yield return new WaitForSeconds(0.1f);
        BattleManager.Instance.isUseSkill = false;
        BattleManager.Instance.isDash = false;




    }

    #endregion

    #region 연속찌르기

    /// <summary>
    /// 연속 찌르기
    /// </summary>
    public void Skill_Sting()
    {
        currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Name] -= Time.deltaTime;

        if (UseSkill(DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Name,
            SkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Name],
            currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Name],
            DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Fmana,
            DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Command))
        {
            ATKDamage = DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Fdmg;
            BattleManager.Instance.isUseSkill = true;
            UseMp(40);
            anim.SetTrigger("Sting");
            currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Name] = SkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Name];
        }



    }
    #endregion

    #region 회전공격
    public void Skill_SpinAttack()
    {
        currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Name] -= Time.deltaTime;
        if (UseSkill(DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Name,
            SkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Name],
            currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Name],
            DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Fmana,
            DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Command))
        {
            ATKDamage = DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Fdmg;
            UseMp(DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Fmana);
            StartCoroutine(SpinAttack());

        }
    }


    IEnumerator SpinAttack()
    {
        currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Name] =
            SkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Name];

        BattleManager.Instance.isUseSkill = true;
        anim.SetBool("isSpinAttack", true);

        yield return new WaitForSeconds(3f);

        anim.SetBool("isSpinAttack", false);
        BattleManager.Instance.isUseSkill = false;


    }
    #endregion

    #region 검이커져!

    public void Skill_BigSword()
    {
        currentSkillcoolTimeDic["BigSword"] -= Time.deltaTime;


        if (UseSkill("검이커져!", SkillcoolTimeDic["BigSword"], currentSkillcoolTimeDic["BigSword"], 40, new List<int>() { 0, 1, 2, 5 }))
        {
            ATKDamage = 20;
            BattleManager.Instance.isUseSkill = true;
            anim.SetTrigger("BigSword");
            UseMp(40);

            currentSkillcoolTimeDic["BigSword"] = SkillcoolTimeDic["BigSword"];
        }
    }




    #endregion
    #endregion


}
