using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimatorController : MonoBehaviour
{
    Animator anim;
    Character character;

    float AttackDelay;
    float currentAttackDelay = 0;
    void Start()
    {
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
        anim.SetBool("isContact", BattleManager.Instance.isContactP);
    }

    void Attack()
    {
        currentAttackDelay += Time.deltaTime; 

        if (BattleManager.Instance.isContactP == true)
        {
            if (BattleManager.Instance.isDash == false)
            {
                if (currentAttackDelay >= AttackDelay)
                {
                    anim.SetTrigger("Attack");
                    Debug.Log("ATK");
                    currentAttackDelay = 0;
                }
            }
        }
    }

    public void SetDash(bool _value)
    {
        anim.SetBool("isDash", _value);
    }
}
