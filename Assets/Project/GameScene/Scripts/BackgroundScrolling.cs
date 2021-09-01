using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public MeshRenderer render;
    public float offset;
    public float speed;
    public float of;
    public Material[] mats;
    Animator anim;
    private void Start()
    {
        speed = 0.35f;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
        if (!BattleManager.Instance.isContact && !BattleManager.Instance.isStop && !BattleManager.Instance.isUseSkill)
        {
            switch (BattleManager.Instance.currentmap)
            {
                case BattleManager.Stage.forest:
                    anim.SetInteger("index", 1);
                    
                    break;
                case BattleManager.Stage.GoblinViliage:
                    anim.SetInteger("index", 2);

                    break;
                default:
                    break;
            }

            foreach (Material i in mats)
            {
                i.mainTextureOffset = new Vector2(offset, 0);

            }
            if (offset >= 100)
            {
                offset = 0;
            }
            offset += Time.deltaTime * speed;

            
        }
    }

    
}
