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
    private void Start()
    {
        speed = 0.7f;
    }

    private void Update()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
        if (!BattleManager.Instance.isContact && !BattleManager.Instance.isStop && !BattleManager.Instance.isUseSkill)
        {
            offset += Time.deltaTime * speed;

            foreach (Material i in mats)
            {
                i.mainTextureOffset = new Vector2(offset, 0);

            }
            if (offset >= 100)
            {
                offset = 0;
            }
        }
    }

    
}
