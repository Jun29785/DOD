using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerater : MonoBehaviour
{
    [SerializeField]
    float SpawnInterval;
    [SerializeField]
    int spawnAmount;
    [SerializeField]
    float currentTime;
    int purpose;
    int monsterNo;
    int Posindex;
    int PrevPosindex;
    bool isBoss;
    void Start()
    {
        purpose = 20;
        PrevPosindex = 99;
        Posindex = 0;
        spawnAmount = 1;
        SpawnInterval = 1;
        monsterNo = 20001;


    }


    void Update()
    {
        


        currentTime += Time.deltaTime;
            
        if (currentTime >= SpawnInterval && !BattleManager.Instance.isBoss)
        {
            for (int i = 0; i < spawnAmount; i++)
            {


                switch (Posindex)
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
                        else if (ScoreCheck(10, 40))
                        {
                            currentTime = 0;
                            Randomindex();
                            RandomMonsterNo(20001, 20003);
                            spawnAmount = Random.Range(1, 4);
                            SpawnInterval = Random.Range(3, 6);
                        }

                        else if (ScoreCheck(40, 100))
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
                }

            }
        }
        
        

            /*if(BattleManager.Score < 200 + purpose )
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

            }*/

    public void Randomindex()
    {
        Posindex = Random.Range(0, 3);

        if (Posindex == PrevPosindex)
        {
            Posindex = Random.Range(0, 3);
        }
    }

    public void RandomMonsterNo(int min, int max)
    {
        monsterNo = Random.Range(20001, 20004);


    }

    public bool ScoreCheck(int min, int max)
    {
        if (BattleManager.Score >= min && BattleManager.Score < max && !BattleManager.Instance.isBoss)
        {
            return true;
        }

        return false;
    }
}