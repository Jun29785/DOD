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
    void Start()
    {
        Debug.Log("Start");
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        AttackDelay = character.AttackDelay;
    }



    void Update()
    {
        Attack();
        BattleCheck();

    }

    void BattleCheck()
    {
        anim.SetBool("isContact", BattleManager.Instance.isContact);
    }

    void Attack()
    {
        currentAttackDelay += Time.deltaTime;


        /*if (BattleManager.Instance.isContact)
        {*/

            Debug.Log("CHECK1");

            if (!BattleManager.Instance.isUseSkill)
            {
                Debug.Log("CHECK2");

                if (currentAttackDelay >= AttackDelay)
                {
                    Debug.Log("CHECK3");
                    anim.SetTrigger("Attack");
                    currentAttackDelay = 0;
                }
            }
      //  }
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
