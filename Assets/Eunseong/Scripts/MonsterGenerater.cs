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
            Objectpool.GetGoblinobject(transform.position);
            currentTime = 0;
        }
    }
}
