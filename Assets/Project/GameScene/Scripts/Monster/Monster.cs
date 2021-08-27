using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DB;
using TMPro;

public abstract class Monster : Actor
{
    [Header("State")]
    public float Speed;
    public float applySpeed;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;


    public bool Stop;
    public bool isDie = false;
    
    public GameObject Hpbarbackground;
    public GameObject CoinPrefab;
    public Image Hpbar;
    public Transform DamageText_SpawnPoint;
    public GameObject canvas;
    bool isBackHp;

    public override void Awake()
    {
        base.Awake();
        DamageText_SpawnPoint.position = new Vector2(DamageText_SpawnPoint.position.x + Random.Range(-0.2f, 0.2f), DamageText_SpawnPoint.position.y);

    }
    public override void Start()
    {
        base.Start();

        if (Hpbar != null)
        {
            Hpbar.fillAmount = 1f;
        }
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

    public override void SetData(int Key)
    {
        unitNo = DataBaseManager.Instance.tdMonsterDict[Key].unitNo;
        Name = DataBaseManager.Instance.tdMonsterDict[Key].Name;
        MaxHp = DataBaseManager.Instance.tdMonsterDict[Key].Hp;
        Power = DataBaseManager.Instance.tdMonsterDict[Key].Power;
        Speed = DataBaseManager.Instance.tdMonsterDict[Key].Speed;
        AttackDelay = DataBaseManager.Instance.tdMonsterDict[Key].AttackDelay;
        AttackDistance = DataBaseManager.Instance.tdMonsterDict[Key].AttackDistance;
        AddScore = DataBaseManager.Instance.tdMonsterDict[Key].AddScore;
        AddCoin = DataBaseManager.Instance.tdMonsterDict[Key].AddCoin;
        
    }
    public virtual void HpUI_Update()
    {
        Hpbar.fillAmount = Mathf.Lerp(Hpbar.fillAmount, Hp / MaxHp, 8f * Time.deltaTime);

    }


    public override void Damaged(float value)
    {
        base.Damaged(value);
        Objectpool.GetDamageText(canvas, DamageText_SpawnPoint.position, ((int)value).ToString());
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
        if (CurrentAttackDelay >= ApplyAttackDelay && !BattleManager.Instance.isDash)
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



    


