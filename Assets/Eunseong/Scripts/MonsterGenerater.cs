using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerater : MonoBehaviour
{
    [SerializeField]
    float SpawnInterval;
    float currentTime;
    int purpose;
    void Start()
    {
        purpose = 200;
    }
    
    
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= SpawnInterval)
        {
            Objectpool.GetMonsterobject(new Vector2(transform.position.x, Random.Range(0.09f, 0.51f)));
            currentTime = 0;
        }
        if(BattleManager.Score < 200 + purpose )
        {

        }
        else if(BattleManager.Score >= 201 + purpose && BattleManager.Score < 400 + purpose)
        {

        }
        else if(BattleManager.Score >= 401 + purpose && BattleManager.Score < 600 + purpose)
        {

        }
        else if (BattleManager.Score >= 601 + purpose)
        {

        }
    }
}
    