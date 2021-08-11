using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectileAttck : MonoBehaviour
{

    public float Speed;
    public float StrikingPower;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.left * Speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BattleManager.Instance.PlayerDamage(StrikingPower);
            Destroy(gameObject);
        }
    }
}
