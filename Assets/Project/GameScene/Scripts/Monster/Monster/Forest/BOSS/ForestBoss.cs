using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : BossMonster
{

    bool isBattle;
    public virtual void BattleCheck() // 배틀 감지
    {
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left, AttackDistance, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {
            isBattle = true;
            TryAttack();
        }
        else
        {
            isBattle = false;
        }


    }
}
