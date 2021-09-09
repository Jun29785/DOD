using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinViliageBoss : BossMonster
{
    float applyPower;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        passive_ATK_increase();
    }


    public void passive_ATK_increase()
    {
        float per = 100 - ((float)Hp / MaxHp) *100;
        applyPower = Power + per / 2;
    }

    public override void Attack()
    {
        BattleManager.Instance.PlayerDamage(applyPower);
    }
}
