using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimatorController : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    float AttackDelay = 0.2f;
    float currentAttackDelay = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AttackCheck(); 
        BattleCheck();
    }

    void BattleCheck()
    {
        anim.SetBool("isBattle", BattleManager.instance.isBattleP);
    }

    void AttackCheck()
    {
        currentAttackDelay += Time.deltaTime;

        if (BattleManager.instance.isBattleP == true)
        {
            if (currentAttackDelay >= AttackDelay)
            {
                anim.SetTrigger("Attack");
                currentAttackDelay = 0;
            }
        }
    }
}
