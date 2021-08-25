using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerater : MonoBehaviour
{
    public GameObject BossPrefab;
    int cnt = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.Instance.isBoss && cnt <= 0)
        {
            Instantiate(BossPrefab);
            cnt++;
        }
    }
}
