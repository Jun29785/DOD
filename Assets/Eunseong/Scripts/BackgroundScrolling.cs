using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    MeshRenderer render;
    float offset;
    public float speed;
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        speed = 0.7f;
    }

    private void Update()
    {
        if (!BattleManager.Instance.isContactP )
        {
            offset += Time.deltaTime * speed;
            render.material.mainTextureOffset = new Vector2(offset, 0);
            if(offset >= 100)
            {
                offset = 0;
            }
        }
    }

    
}
