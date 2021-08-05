using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Knight : MonoBehaviour
{
    public Transform originPos; //원래 위치(돌진)
    Character character;
    public Transform DashTransform; // 돌진으로 가야하는 위치(돌진)
    int FirstTargetcount = 0; // 첫번째 공격상대인지 판별하기위한 변수
    Collider2D target;// 공격타겟
    KnightAnimatorController animController;

    Dictionary<string,float> SkillcoolTimeDic = new Dictionary<string,float>();
    Dictionary<string,float> SkillcoolTimeDic2 = new Dictionary<string,float>();
    bool isDash = false;
    void Awake()
    {
        character = GetComponent<Character>();
        animController = GetComponent<KnightAnimatorController>();
        SkillcoolTimeDic.Add("Dash", 0);
        SkillcoolTimeDic.Add("Sting", 0);

        SkillcoolTimeDic2.Add("Dash", 3);
        SkillcoolTimeDic2.Add("Sting", 5);
    }

    void Update()
    {
        character.DieCheck();
        Skill_Dash();
        Skill_Sting();
        BattleCheck();

/*        for (int i = 0; i < BattleManager.Instance.Pattern_id.Count; i++)
        {
            print(i + "   "+BattleManager.Instance.Pattern_id[i]);

        }*/
    }

    private void FixedUpdate()
    {
        ConcactCheck();
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right);

    }

    private void BattleCheck()
    {
       // animController.SetBool("isContact", BattleManager.Instance.isContact);
    }


    #region 판정
    void SingleATK() // 단일공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(character.attackTransform.position, character.AttackRange, LayerMask.GetMask("Monster"));
        
        foreach (Collider2D enemy in hitEnemy)
        {

            
            if (FirstTargetcount == 0)
            {

                target = enemy;
            }

            FirstTargetcount++;

            if (target.gameObject.activeSelf == false)
            {
                FirstTargetcount = 0;
            }


            target.GetComponent<Monster>().Monster_Damage(character.StrikingPower);
            break;
        }
    }
    void MultiATK() // 다중공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(character.attackTransform.position, character.AttackRange /*스킬 정보에서 빼오기 */, LayerMask.GetMask("Monster"));
        
        foreach (Collider2D enemy in hitEnemy)
        {
            
            enemy.GetComponent<Monster>().Monster_Damage(3);
        }
    }


    #endregion
    void ConcactCheck() // 배틀상태 감지
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, character.AttackDistance,LayerMask.GetMask("Monster"));

        if (hitinfo.collider != null && isDash == false)
        {
            BattleManager.Instance.isContact = true;
    //      hitinfo.collider.GetComponent<Monster>().Stop = true;
        }
        else
        {
            BattleManager.Instance.isContact = false;
 /*           GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");*/

            /*foreach (GameObject monster in Monsters)
            {
                //              monster.GetComponent<Monster>().Stop = false;
            }*/
        }
    }

    void SetUseSkillFalse()
    {
        BattleManager.Instance.isUseSkill = false;
    }





    private void OnDrawGizmosSelected()//기즈모그리기
    {

        Gizmos.DrawWireSphere(character.attackTransform.position, character.AttackRange);

    }


    #region 스킬


    /// <summary>
    /// 돌진
    /// </summary>
    public void Skill_Dash()
    {
        SkillcoolTimeDic["Dash"] -= Time.deltaTime;

        if (BattleManager.Instance.Pattern_id.SequenceEqual( new List<int>(){3,4,5} ))
        {

            if (BattleManager.Instance.PatternInputEnd){

                if (SkillcoolTimeDic["Dash"] <= 0)
                {

                    if (isDash == false && !BattleManager.Instance.isUseSkill)
                    {
                        Debug.Log("Dash");
                        StartCoroutine(DashCroutine());
                    }
                }
            }
        }
    }
    

    /// <summary>
    /// 연속 찌르기
    /// </summary>
    public void Skill_Sting()
    {
        SkillcoolTimeDic["Sting"] -= Time.deltaTime;

        if (BattleManager.Instance.Pattern_id.SequenceEqual(new List<int>() { 0,1,2 }) && !BattleManager.Instance.isUseSkill && character.Mp - 40 >= 0)
        {
            if (SkillcoolTimeDic["Sting"] <= 0)
            {
                character.Mp -= 40;
                BattleManager.Instance.isUseSkill = true;
                animController.SetSkillTrigger("Sting");
                SkillcoolTimeDic["Sting"] = SkillcoolTimeDic2["Sting"];
            }
        }
        
    }

    /// <summary>
    /// 돌진 판정
    /// </summary>
    IEnumerator DashCroutine()
    {
        float currentTime = 1;
        if (character.Mp - 20 >= 0/*스킬 정보에서 가져오기*/)
        {
            isDash = true;
            BattleManager.Instance.isUseSkill = true;

            character.UseMp(20 /*스킬 정보에서 가져오기*/);
            animController.SetSkillBool("isDash",true);
            while (transform.position.x < DashTransform.position.x - 0.2f)
            {
                currentTime += Time.deltaTime;
                BattleManager.Instance.isUseSkill = true;
                Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(character.attackTransform.position, 0.2f, LayerMask.GetMask("Monster"));
                foreach (Collider2D enemy in hitEnemy)
                {
                    enemy.transform.position = new Vector2(transform.position.x + 0.6f, enemy.transform.position.y);
                    if (currentTime >= 0.2f)
                    {
                        enemy.GetComponent<Monster>().Monster_Damage(character.StrikingPower /* 스킬 데미지 가져오기*/);
                    }

                }
                transform.position = Vector2.Lerp(transform.position, DashTransform.position, 0.02f);
                if (currentTime >= 0.2f)
                {

                    currentTime = 0;
                }
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            animController.SetSkillBool("isDash", false);

            while (transform.position.x > originPos.transform.position.x + 0.1f)
            {

                transform.position = Vector2.Lerp(transform.position, originPos.transform.position, 0.08f);
                yield return null;
            }


            BattleManager.Instance.isUseSkill = false;
            transform.position = originPos.position;
            isDash = false;
            SkillcoolTimeDic["Dash"] = SkillcoolTimeDic2["Dash"];
            StopAllCoroutines();
        }
        else
        {
            StopAllCoroutines();
        }
    }

    #endregion
}
