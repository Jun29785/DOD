﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{

    public string MonsterName;
    public float AttackDelay;
    public float AttackDistance;
    public float MaxHp;
    public float Hp;
    public float Speed;
    [HideInInspector]
    public float currentDelay;
    public float StrikingPower;
    public float AddScore;
    public int CoinAmount;
    public bool Stop;
    bool isDie = false;

    private BattleManager BattleManager;

    public GameObject Hpbarbackground;
    public GameObject CoinPrefab;
    public Image Hpbar;
    Animator anim;
    public Text DamageText;
    void Start()
    {
        currentDelay = AttackDelay;
        Hp = MaxHp;
        Hpbar.fillAmount = 1f;
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Hp = MaxHp;
        isDie = false;
    }
    void Update()
    {
        currentDelay += Time.deltaTime;

        isContactCheck();
        HpUI_Update();
        Move();
        DieCheck();
    }

    private void FixedUpdate()
    {
        TryAttack();
    }



    public void Monster_Damage(float value)
    {
        Hp -= value;
        anim.SetTrigger("Damaged");
    }
        
    void HpUI_Update()
    {
        Hpbar.fillAmount = Hp / MaxHp;
    }
    void TryAttack()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left);
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left, AttackDistance, LayerMask.GetMask("Player"));
        
        if (rayhit.collider != null)
        {

            Stop = true;

            if (currentDelay >= AttackDelay)
            {
                anim.SetTrigger("Attack");
                currentDelay = 0;
            }
        }
        else
        {
            Stop = false;
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
            anim.SetBool("isDie",true);
            if (!isDie)
            {
                Invoke("Die", 1);
                isDie = true;
            }
        }
    }

    public void Die()
    {
        GameSceneUIManager.Instance.GetScore(AddScore);
        for (int i = 0; i < 3; i++)
        {
            Objectpool.GetCoinobject(new Vector2(transform.position.x, transform.position.y + 0.5f));
            //Instantiate(CoinPrefab, new Vector2(transform.position.x, transform.position.y + 0.6f), transform.rotation);
        }

        BattleManager.Instance.GetGold(CoinAmount);
        Objectpool.ReturnMonster(this);
    }

    public void Attack()
    {
        BattleManager.Instance.PlayerDamage(StrikingPower);
    }

    public void isContactCheck()
    {
        anim.SetBool("isContact", BattleManager.Instance.isContact);
    }
}
