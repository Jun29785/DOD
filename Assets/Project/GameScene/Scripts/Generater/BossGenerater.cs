using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerater : MonoBehaviour
{
    public GameObject ForestBossPrefab;
    public GameObject GoblinBossPrefab;
    int cnt = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (BattleManager.Instance.Score >= BattleManager.Instance.purpose && cnt <= 0)
        {
            BattleManager.Instance.isBoss = true;
            switch (BattleManager.Instance.currentmap)
            {
                case BattleManager.Stage.forest:
                    Instantiate(ForestBossPrefab, transform.position, Quaternion.identity);

                    break;
                case BattleManager.Stage.GoblinViliage:
                    Instantiate(GoblinBossPrefab,transform.position, Quaternion.identity);

                    break;
                default:
                    break;
            }
            cnt++;
        }
        if (!BattleManager.Instance.isBoss)
        {
            cnt = 0;
        }
    }
}
