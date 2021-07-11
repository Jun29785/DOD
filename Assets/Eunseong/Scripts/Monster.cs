using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string MonsterName;
    public float AttackDelay;
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float currentDelay;
    public float StrikingPower;
    void Start()
    {
        currentDelay = AttackDelay;
        Hp = MaxHp;
    }

    void Update()
    {
        currentDelay += Time.deltaTime;


        if (BattleManager.instance.isBattleP) {
            if (currentDelay >= AttackDelay)
            {
                BattleManager.instance.PlayerDamage(StrikingPower);
                currentDelay = 0;
            }
        }

        Move();
        DieCheck();
    }

    public void Monster_Damage(float value)
    {
        Hp -= value;
    }


    public void Move()
    {
        if (BattleManager.instance.isBattleP == false)
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
    }

    public void DieCheck()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
