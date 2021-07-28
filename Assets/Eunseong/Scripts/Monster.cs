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
    public float DropCoinAmount;
    public bool Stop;
    bool Die;

    private BattleManager BattleManager;

    public GameObject Hpbarbackground;
    public GameObject CoinPrefab;
    public Image Hpbar;
    Rigidbody2D rigid;
    void Start()
    {
        currentDelay = AttackDelay;
        Hp = MaxHp;
        Hpbar.fillAmount = 1f;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Hp = MaxHp;
    }
    void Update()
    {
        currentDelay += Time.deltaTime;


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
            Debug.Log(rayhit.collider.name);

            Stop = true;
            BattleManager.Instance.isContact = true;
            if (currentDelay >= AttackDelay)
            {
                BattleManager.Instance.PlayerDamage(StrikingPower);
                currentDelay = 0;
            }
        }
        else
        {
            Stop = false;
            BattleManager.Instance.isContactP = false;
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
            for (int i = 0; i < DropCoinAmount; i++)
            {
                Objectpool.GetCoinobject(new Vector2(transform.position.x, transform.position.y + 0.5f));
                //Instantiate(CoinPrefab, new Vector2(transform.position.x, transform.position.y + 0.6f), transform.rotation);
            }

            Objectpool.ReturnGoblin(this);

        }
    }


   
}
