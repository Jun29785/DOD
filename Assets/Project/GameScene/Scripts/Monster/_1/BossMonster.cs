using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossMonster : Monster
{
    public GameObject BossState;
    public Slider bossHpbar;
    public Text bossName;

    public GameObject[] BossUIPos;
    bool isApear;

    int cnt; // 맞은 횟수 
    public override void Awake()
    {
        base.Awake();
        BossState = GameObject.FindGameObjectWithTag("BossUI");
        bossHpbar = BossState.GetComponentInChildren<Slider>();
        bossName = BossState.GetComponentInChildren<Text>();
        BossUIPos = GameObject.FindGameObjectsWithTag("BossUIPos");
    }

    public override void Start()
    {
        anim = GetComponent<Animator>();
        applySpeed = Speed;
        ApplyAttackDelay = AttackDelay;
    }


    public override void Update()
    {
        CurrentAttackDelay += Time.deltaTime;

        if (isApear)
        {
            SetHp(Hp, MaxHp);
        }
        Move();
        DieCheck();
        animUpdate();

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

    public override void Die()
    {
        this.gameObject.SetActive(false);
    }

    public override void Damaged(float value)
    {
        Objectpool.GetDamageText(canvas, DamageText_SpawnPoint.position, ((int)value).ToString());
        if(cnt >= 5) 
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


    public override void animUpdate()
    {
        base.animUpdate();
    }

    public override void SetData(int Key)
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnable()
    {
        bossName.text = this.Name.ToString();
        BossState.transform.position = BossUIPos[0].transform.position;

        base.OnEnable();
        bossHpbar.value = 0;
        StartCoroutine(appear());
    }


    IEnumerator appear()
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
        bossHpbar.value = Mathf.Lerp(bossHpbar.value, hp / maxhp, 3 * Time.deltaTime);
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

    private void OnDisable()
    {
        BossState.transform.position = BossUIPos[1].transform.position;
        BattleManager.Instance.isBoss = false;
        BattleManager.Instance.purpose += BattleManager.Instance.BossInterval;
        BattleManager.Instance.Panel.GetComponent<Animator>().SetTrigger("isStart");
        BattleManager.Instance.nextStage();
        BattleManager.Instance.SetmonsterGenInterval(5f);
    }
}