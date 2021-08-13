using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Monster : Actor
{

    public float Speed;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;
    public bool Stop;
    bool isDie = false;



    public GameObject Hpbarbackground;
    public GameObject CoinPrefab;
    public Image Hpbar;
    public Text DamageText;


    public override void Start()
    {
        base.Start();
        Hpbar.fillAmount = 1f;

    }

    public virtual void OnEnable()
    {
        Hp = MaxHp;
        isDie = false;
    }

    public override void Update()
    {
        base.Update();
        DieCheck();
        HpUI_Update();
        Move();
    }



    void HpUI_Update()
    {
        Hpbar.fillAmount = Hp / MaxHp;
    }

    public abstract void Move();

    public override void DieCheck()
    {
        if (Hp <= 0)
        {
            anim.SetBool("isDie", true);
            if (!isDie)
            {
                Invoke("Die", 0.5f);
                isDie = true;
            }
        }
    }

    public virtual void Die()
    {
        GameSceneUIManager.Instance.GetScore(AddScore);
        for (int i = 0; i < 3; i++)
        {
            Objectpool.GetCoinobject(new Vector2(transform.position.x, transform.position.y + 0.5f));
        }

        BattleManager.Instance.GetGold(AddCoin);
        Objectpool.ReturnMonster(this);
    }



    public override void animUpdate()
    {

        anim.SetBool("isContact", Stop);
    }

    public virtual void ContactCheck() // 맞닿음 감지
    {
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left, 0.5f, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {

            Stop = true;

        }
        else
        {
            Stop = false;
        }
    }

    public virtual void TryAttack()
    {
        if (CurrentAttackDelay >= AttackDelay)
        {
            anim.SetTrigger("Attack");
            CurrentAttackDelay = 0;
        }
    }

    public virtual void Attack()
    {
        BattleManager.Instance.PlayerDamage(Power);
    }

}



    


