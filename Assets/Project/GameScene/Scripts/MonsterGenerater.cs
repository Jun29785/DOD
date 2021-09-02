using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerater : MonoBehaviour
{
    [SerializeField]
    public float SpawnInterval;
    [SerializeField]
    int spawnAmount;
    [SerializeField]
    public float currentTime;
    int monsterNo;
    int Posindex;
    int PrevPosindex;

    void Start()
    {
        PrevPosindex = 99;
        Posindex = 0;
        spawnAmount = 1;
        SpawnInterval = 5;
        monsterNo = 20001;
    }


    void Update()
    {
        currentTime += Time.deltaTime;
        Spawn();

    }

    void Spawn()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
        if (BattleManager.Instance.isStart)
        {
            if (currentTime >= SpawnInterval && !BattleManager.Instance.isBoss)
            {
                for (int i = 0; i < spawnAmount; i++)
                {
                    switch (Posindex) // 몬스터 위치 인덱스의 위치
                    {
                        case 0:
                            Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, 0.51f));
                            break;
                        case 1:
                            Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, 0.35f));
                            break;
                        case 2:
                            Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, 0.123f));
                            break;

                        default:
                            Objectpool.GetMonsterobject(monsterNo, new Vector2(transform.position.x, Random.Range(0.09f, 0.51f)));
                            break;
                    }
                    //Objectpool.GetMonsterobject(monsterNo,new Vector2(transform.position.x, Random.Range(0.09f, 0.51f)));
                    if (ScoreCheck(0, 10))
                    {
                        currentTime = 0;
                        Randomindex();
                        monsterNo = 20001;
                        spawnAmount = Random.Range(1, 3);
                        SpawnInterval = Random.Range(4, 7);
                    }
                    else if (ScoreCheck(10, 25))
                    {
                        currentTime = 0;
                        Randomindex();
                        RandomMonsterNo(20001, 20003);
                        spawnAmount = Random.Range(1, 4);
                        SpawnInterval = Random.Range(3, 6);
                    }

                    else if (ScoreCheck(25, 40))
                    {
                        if (currentTime >= SpawnInterval)
                        {
                            currentTime = 0;
                            Randomindex();
                            monsterNo = 20003;
                            RandomMonsterNo(20001, 20004);
                            spawnAmount = Random.Range(2, 4);
                            SpawnInterval = Random.Range(3, 6);

                        }
                    }
                    else if (ScoreCheck(40, 60))
                    {
                        if (currentTime >= SpawnInterval)
                        {
                            currentTime = 0;
                            Randomindex();
                            RandomMonsterNo(20001, 20004);
                            spawnAmount = Random.Range(2, 4);
                            SpawnInterval = Random.Range(3, 6);

                        }
                    }
                    else if (ScoreCheck(80, 100))
                    {
                        if (currentTime >= SpawnInterval)
                        {
                            currentTime = 0;
                            Randomindex();
                            RandomMonsterNo(20001, 20004);
                            spawnAmount = Random.Range(2, 4);
                            SpawnInterval = Random.Range(3, 6);

                        }
                    }
                    else if (ScoreCheck(100, 120))
                    {
                        if (currentTime >= SpawnInterval)
                        {
                            currentTime = 0;
                            Randomindex();
                            RandomMonsterNo(20004, 20005);
                            spawnAmount = Random.Range(2, 4);
                            SpawnInterval = Random.Range(3, 6);

                        }
                    }
                    else if (ScoreCheck(120, 140))
                    {
                        if (currentTime >= SpawnInterval)
                        {
                            currentTime = 0;
                            Randomindex();
                            RandomMonsterNo(20004, 20006);
                            spawnAmount = Random.Range(2, 4);
                            SpawnInterval = Random.Range(3, 6);

                        }
                    }
                    else if (ScoreCheck(140, 200))
                    {
                        if (currentTime >= SpawnInterval)
                        {
                            currentTime = 0;
                            Randomindex();
                            RandomMonsterNo(20004, 20009);
                            spawnAmount = Random.Range(2, 4);
                            SpawnInterval = Random.Range(3, 6);

                        }
                    }

                    else
                    {
                        print("hello world");
                        if (currentTime >= SpawnInterval)
                        {
                            currentTime = 0;

                            Randomindex();
                            RandomMonsterNo(20001, 20009);
                            spawnAmount = Random.Range(2, 4);
                            SpawnInterval = Random.Range(3, 6);
                        }
                    } 
                }

            }
        }
    }
    public void Randomindex() // 몬스터 위치 인덱스 설정 
    {
        Posindex = Random.Range(0, 3);

        if (Posindex == PrevPosindex)
        {
            for (; Posindex != PrevPosindex;)
            {

                Posindex = Random.Range(0, 3);
            }
        }
    }

    public void RandomMonsterNo(int min, int max) // 몬스터 init num
    {
        monsterNo = Random.Range(min, max);
    }

    public bool ScoreCheck(int min, int max) 
    {
        if (BattleManager.Instance.Score + (BattleManager.Instance.purpose-BattleManager.Instance.BossInterval)>= min && BattleManager.Instance.Score + (BattleManager.Instance.purpose - BattleManager.Instance.BossInterval) < max && !BattleManager.Instance.isBoss)
        {
            return true;
        }

        return false;
    }
}