using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerater : MonoBehaviour
{
    [SerializeField]
    float SpawnInterval;
    float currentTime;
    void Start()
    {
        
    }
    

    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= SpawnInterval)
        {
            Objectpool.GetMonsterobject(new Vector2(transform.position.x, Random.Range(-0.35f,0.05f)));  
            currentTime = 0;
        }
    }
}
    