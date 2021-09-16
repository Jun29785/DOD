using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.Define;
using DOD.DB;

public class BossMonster : Monster
{
    public GameObject BossState;
    public Slider bossHpbar;
    public Text bossName;
    public Text hpText;

    public GameObject[] BossUIPos;
    public bool isApear;
    public float SkillDelay;


    public int cnt; // 맞은 횟수 
    public override void Awake()
    {
        base.Awake();

        BossState = GameObject.FindGameObjectWithTag("BossUI");
        bossHpbar = BossState.GetComponentInChildren<Slider>();
        bossName = GameObject.FindGameObjectWithTag("bossName").GetComponent<Text>();
        hpText = GameObject.FindGameObjectWithTag("bossHp").GetComponent<Text>();
        BossUIPos = GameObject.FindGameObjectsWithTag("BossUIPos");

    }

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        applySpeed = Speed;
        ApplyAttackDelay = AttackDelay;

    }


    public override void Update()
    {
        CurrentAttackDelay += Time.deltaTime;

        if (Hp >= applyMaxHp)
        {
            Hp = applyMaxHp;
        }
        if (isApear)
        {
            SetHp(Hp, MaxHp);
        }
        Move();
        DieCheck();
        animUpdate();
        hpUpdate();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        ContactCheck();
    }


    public override void DieCheck()
    {
        base.DieCheck();
    }


    public override void OnEnable()
    {
        base.OnEnable();
        Hp = applyMaxHp;
        applyPower = Power;
        bossName.text = Name.ToString();


        applyMaxHp = MaxHp;
        Hp = applyMaxHp;


        BossState.transform.position = BossUIPos[0].transform.position;

        bossHpbar.value = 0;
        StartCoroutine(appear());

    }
    public override void Die()
    {
        if (!BattleManager.Instance.isEnd)
        {
            if (BossUIPos[1] != null)
            {
                BossState.transform.position = BossUIPos[1].transform.position;
            }
            BattleManager.Instance.isBoss = false;
            BattleManager.Instance.purpose += BattleManager.Instance.BossInterval;
            BattleManager.Instance.startPanel.GetComponent<Animator>().SetTrigger("isStart");
            BattleManager.Instance.nextStage();
            BattleManager.Instance.SetmonsterGenInterval(5f);

        }
        Objectpool.ReturnBossMonster(this);
    }

    public void OnDisable()
    {
        BossState.transform.position = BossUIPos[1].transform.position;
        setMonsterStat();
        applyPower = Power;
        applyMaxHp = MaxHp;
    }

    public override void Damaged(float value)
    {
        Objectpool.GetDamageText(canvas, DamageText_SpawnPoint.position, ((int)value).ToString());
        if (cnt >= 5)
        {
            Hp -= value;
            anim.SetTrigger("Damaged");
            cnt = 0;
        }
        else
        {
            Hp -= value;

        }
        cnt++;

    }

    public void hpUpdate()
    {
        hpText.text = $"{Hp} / {MaxHp}";
        if (Hp < 0)
        {
            Hp = 0;
        }
    }

    public override void animUpdate()
    {
        base.animUpdate();
    }

    public override void SetData(int Key)
    {
        this.unitNo = DataBaseManager.Instance.tdBossDict[Key].UnitNo;
        this.Name = DataBaseManager.Instance.tdBossDict[Key].Name;
        this.MaxHp = DataBaseManager.Instance.tdBossDict[Key].HP;
        this.applyMaxHp = DataBaseManager.Instance.tdBossDict[Key].HP;
        this.Power = DataBaseManager.Instance.tdBossDict[Key].Power;
        this.applyPower = DataBaseManager.Instance.tdBossDict[Key].Power;
        this.Speed = DataBaseManager.Instance.tdBossDict[Key].Speed;
        this.AttackDistance = DataBaseManager.Instance.tdBossDict[Key].attackDistance;
        this.AttackDelay = DataBaseManager.Instance.tdBossDict[Key].attakDelay;
        this.ApplyAttackDelay = DataBaseManager.Instance.tdBossDict[Key].attakDelay;
        this.AddScore = DataBaseManager.Instance.tdBossDict[Key].AddScore;
        this.AddCoin = DataBaseManager.Instance.tdBossDict[Key].AddCoin;
        this.CoinAmount = DataBaseManager.Instance.tdBossDict[Key].CoinAmount;
        this.SkillDelay = DataBaseManager.Instance.tdBossDict[Key].SkillDelay;
    }

   


    public IEnumerator appear()
    {
        while (bossHpbar.value <= 0.99f)
        {
            yield return null;
            bossHpbar.value = Mathf.Lerp(bossHpbar.value, 1, 3 * Time.deltaTime);
        }
        isApear = true;

        StopCoroutine(appear());
    }

    public void SetHp(float hp, float maxhp)
    {
        bossHpbar.value = Mathf.Lerp(bossHpbar.value, hp / maxhp, 6 * Time.deltaTime);
    }

    public override void Move()
    {
        if (!Stop)
        {
            transform.Translate(Vector2.left * applySpeed * Time.deltaTime);
        }
    }






    public override void ContactCheck() // 공격 감지
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left);
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left, AttackDistance, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {

            Stop = true;
            TryAttack();
        }
        else
        {
            Stop = false;
        }


        

    }



}   