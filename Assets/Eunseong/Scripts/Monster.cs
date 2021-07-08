using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    float AttackDelay = 3;
    float currentDelay;
    float Hp = 5;
    float Speed = 4f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay += Time.deltaTime;


        if (BattleManager.instance.isBattleP) {
            if (currentDelay >= AttackDelay)
            {
                BattleManager.instance.PlayerDamage(1);
                currentDelay = 0;
            }
        }

        Move();
        Die();
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

    public void Die()
    {
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
