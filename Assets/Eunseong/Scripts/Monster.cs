using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{

    public string MonsterName;
    public float AttackDelay;
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float currentDelay;
    public float StrikingPower;
    public float AddScore;
    public bool Stop;

    private BattleManager BattleManager;

    public GameObject Hpbarbackground;
    public Image Hpbar;
    void Start()
    {
        currentDelay = AttackDelay;
        Hp = MaxHp;
        Hpbar.fillAmount = 1f;
        
    }

    void Update()
    {
        currentDelay += Time.deltaTime;


        HpUI_Update();
        TryAttack();
        Move();
        DieCheck();
    }

    
    


    public void Monster_Damage(float value)
    {
        Hp -= value;
    }
        
    void HpUI_Update()
    {
        Hpbar.fillAmount = Hp / MaxHp;
    }
    void TryAttack()
    {
        if (BattleManager.Instance.isContactP)
        {
            if (currentDelay >= AttackDelay)
            {
                BattleManager.Instance.PlayerDamage(StrikingPower);
                currentDelay = 0;
            }
        }
    }
    public void Move()
    {
        if (!Stop)
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
    }

    public void DieCheck()
    {
        if (Hp <= 0)
        {
            UIManager.Instance.GetScore(AddScore);
            Destroy(gameObject);
        }
    }
}
