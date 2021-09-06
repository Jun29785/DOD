using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_SwordAura_Projectile : MonoBehaviour
{
    public Vector2 AttackRange;
    float Power;
    float moveSpeed;

    int cnt;
    public void Init(float power, float speed)
    {
        Power = power;
        moveSpeed = speed;

    }

    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, AttackRange);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (cnt == 0)
        {
            if (collision.gameObject.layer == LayerMask.GetMask("Monster"))
            {
                Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(transform.position, AttackRange, LayerMask.GetMask("Monster")); // 콜라이더를 받아와서
                if (hitEnemy != null)
                {
                    cnt++;

                    foreach (Collider2D monster in hitEnemy)
                    {
                        monster.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 0));
                        monster.gameObject.GetComponent<Monster>().Damaged(Power);

                    }


                    Destroy(gameObject);
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (cnt == 0)
        {
            if (collision.gameObject.layer == LayerMask.GetMask("Monster"))
            {
                Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(transform.position, AttackRange, LayerMask.GetMask("Monster")); // 콜라이더를 받아와서
                if (hitEnemy != null)
                {
                    cnt++;

                    foreach (Collider2D monster in hitEnemy)
                    {
                        monster.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 0));
                        monster.gameObject.GetComponent<Monster>().Damaged(Power);

                    }


                    Destroy(gameObject);
                }
            }
        }
    }
}
