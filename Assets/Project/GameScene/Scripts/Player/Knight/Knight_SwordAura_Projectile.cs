using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_SwordAura_Projectile : MonoBehaviour
{
    public Vector2 AttackRange;
    float Power;
    float moveSpeed;

    public void Init(float power,float speed)
    {
        Power = power;
        moveSpeed = speed;
        
    }

    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(transform.position, AttackRange, LayerMask.GetMask("Monster")); // 콜라이더를 받아와서
       if(hitEnemy != null)
        {
            foreach (Collider2D monster in hitEnemy)
            {
                monster.gameObject.GetComponent<Monster>().Damaged(Power);
                monster.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 0));
            }


            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, AttackRange);

    }
}
