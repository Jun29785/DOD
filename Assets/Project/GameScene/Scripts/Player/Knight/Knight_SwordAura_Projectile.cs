using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_SwordAura_Projectile : MonoBehaviour
{
    public Vector2 AttackRange;
    float Power;
    float moveSpeed;

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

        if (collision.tag == "Monster")
        {
            //Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(transform.position, AttackRange, LayerMask.GetMask("Monster")); // 콜라이더를 받아와서
            //if (hitEnemy != null)
            // {

            //foreach (Collider2D monster in hitEnemy)
            //{
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x + 20, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    collision.gameObject.GetComponent<Monster>().Damaged(1);

                //}


                Destroy(gameObject);
            //}
        }


    }
}