using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    float Hp = 3;
    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x,transform.position.y + 0.5f), Vector2.right);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y +0.5f), Vector2.right, 0.5f);

        if(hitinfo.collider != null)
        {
            BattleManager.instance.isBattleP = true;
        }
        else
        {
            BattleManager.instance.isBattleP = false;
        }
        
    }

    public void Damage(float value)
    {
        Hp -= value;
        Debug.Log(this.Hp);
    }
}
