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
            Objectpool.GetMonsterobject(new Vector2(transform.position.x, Random.Range(0.1f, 0.5f)));  
            currentTime = 0;
        }
    }
}
    