using System.Collections;
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
    public int AddCoin;
    public int CoinAmount;
//    public AttackType atkType;


    public bool Stop;
    bool isDie = false;

  /*  public enum AttackType
    {
        meleeAttack,
        projectileAttack,
    }*/


    public GameObject Hpbarbackground;
    public GameObject CoinPrefab;
    public Image Hpbar;
    Animator anim;
    public Text DamageText;

    public GameObject projectilePrefab;

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

        BattleManager.Instance.GetGold(AddCoin);
        Objectpool.ReturnMonster(this);
    }

    public void Attack()
    {
        BattleManager.Instance.PlayerDamage(StrikingPower);
    }

    public void isContactCheck()
    {
        anim.SetBool("isContact", Stop);
    }



    // 근접 공격

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

    void projectileTryAttack()
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

    public void Create_projectile()
    {
        var obj = Instantiate(projectilePrefab, transform.position, transform.rotation);
        obj.GetComponent<MonsterProjectileAttck>().StrikingPower = this.StrikingPower;
    }
}

