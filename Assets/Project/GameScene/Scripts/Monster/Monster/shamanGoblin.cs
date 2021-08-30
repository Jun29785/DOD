using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shamanGoblin : ProjectileMonster
{


    public Vector2 healArea;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }



    public void shamanHeal()
    {
        Collider2D[] healMonster = Physics2D.OverlapBoxAll(transform.position, healArea, LayerMask.GetMask("Monster"));


        foreach (Collider2D monster in healMonster)
        {
            monster.GetComponent<Monster>().Hp *= 1.5f;

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, healArea);
    }
}
