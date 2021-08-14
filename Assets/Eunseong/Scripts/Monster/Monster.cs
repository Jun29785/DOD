using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Monster : Actor
{

    public float Speed;
    public float applySpeed;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;
    public bool Stop;
    bool isDie = false;



    public GameObject Hpbarbackground;
    public GameObject CoinPrefab;
    public Image Hpbar;
    public Image BackHpbar;
    public Text DamageText;
    bool isBackHp;

    public override void Start()
    {
        base.Start();
        Hpbar.fillAmount = 1f;
        applySpeed = Speed;
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

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void HpUI_Update()
    {
        Hpbar.fillAmount = Mathf.Lerp(Hpbar.fillAmount, Hp / MaxHp, 8f * Time.deltaTime);

        if (isBackHp)
        {
            BackHpbar.fillAmount = Mathf.Lerp(BackHpbar.fillAmount, Hp / MaxHp, 10f * Time.deltaTime);

            if (Hpbar.fillAmount >=BackHpbar.fillAmount - 0.001f)
            {
                isBackHp = false;
                BackHpbar.fillAmount = Hpbar.fillAmount;
            }
        }
    }

    void BackHpFunc()
    {
        isBackHp = true;
    }

    public override void Damaged(float value)
    {
        base.Damaged(value);
        Invoke("BackHpFunc", 0.01f);
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
        if (CurrentAttackDelay >= ApplyAttackDelay)
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



    


