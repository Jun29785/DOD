using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimatorController : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    Character character;

    float AttackDelay;
    public float currentAttackDelay = 0;
    bool CONTACT;
    void Start()
    {
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        AttackDelay = character.AttackDelay;
    }

        

    void Update()
    {
        BattleCheck();
        Attack();
    }
 

    void BattleCheck()
    {

        anim.SetBool("isContact", BattleManager.Instance.isContact);
        
    }

    void Attack()
    {
        currentAttackDelay += Time.deltaTime;

        
       if (BattleManager.Instance.isContact)
        {


            if (!BattleManager.Instance.isUseSkill)
            {

                if (currentAttackDelay >= AttackDelay)
                {
                     anim.SetTrigger("Attack");

                    currentAttackDelay = 0;
                }
            }
        }
    }

    public void SetSkillBool(string name, bool _value)
    {
        anim.SetBool(name, _value);
    }

    public void SetSkillTrigger(string name)
    {
        anim.SetTrigger(name);
    }

}
