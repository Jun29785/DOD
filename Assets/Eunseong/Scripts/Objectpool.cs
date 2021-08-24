using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Objectpool : MonoBehaviour
{
    public static Objectpool Instance;

    [SerializeField]
    private GameObject CoinPrefabs;
    [SerializeField]
    private GameObject[] MonsterPrefabs;
    [SerializeField]
    private GameObject DamageText;

    private Queue<Coin>CoinQueue = new Queue<Coin>();
    private Queue<Monster> MonsterQueue = new Queue<Monster>();
    private Queue<DamageText> DamageTextQueue = new Queue<DamageText>();
    void Start()
    {
        Instance = this;
        Initialize(50);
    }

    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CoinQueue.Enqueue(CreateNewCoin());
            CreateNewMonsters();
        }

        for (int i = 0; i < 10000; i++)
        {
            DamageTextQueue.Enqueue(CreateNewDamageText());
        }
    }

    #region Coin

    private Coin CreateNewCoin()
    {
        var newObj = Instantiate(CoinPrefabs).GetComponent<Coin>();
        newObj.transform.parent = Instance.transform;
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static Coin GetCoinobject(Vector2 pos)
    {
        if (Instance.CoinQueue.Count > 0)
        {
            var obj = Instance.CoinQueue.Dequeue();
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewCoin();
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnCoin(Coin Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.CoinQueue.Enqueue(Obj);
    }

    #endregion

    #region Monster

    private void CreateNewMonsters()
    {

        foreach (GameObject monster in MonsterPrefabs)
        {
            var newObj = Instantiate(monster).GetComponent<Monster>();
            newObj.transform.parent = Instance.transform;
            newObj.gameObject.SetActive(false);
            MonsterQueue.Enqueue(newObj);
        }
        

    }



    public static Monster GetMonsterobject(int monsterNo,  Vector2 pos/*, BattleManager.Monster type*/)
    {
        if (Instance.MonsterQueue.Count > 0)
        {
            var obj = Instance.MonsterQueue.Dequeue();
            while(obj.GetComponent<Monster>().unitNo != monsterNo)
            {
                Instance.MonsterQueue.Enqueue(obj);
                obj = Instance.MonsterQueue.Dequeue();
            }
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            Instance.CreateNewMonsters();
            var newObj = Instance.MonsterQueue.Dequeue();
            while (newObj.GetComponent<Monster>().unitNo != monsterNo)
            {
                Instance.MonsterQueue.Enqueue(newObj);
                newObj = Instance.MonsterQueue.Dequeue();
                newObj.transform.position = pos;
            
            }
                newObj.transform.position = pos;

            newObj.gameObject.SetActive(true);
            return newObj;



        }
    }

    public static void ReturnMonster(Monster Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.MonsterQueue.Enqueue(Obj);
    }

    #endregion

    #region DamageText

    private DamageText CreateNewDamageText()
    {
        var newObj = Instantiate(DamageText).GetComponent<DamageText>();
        newObj.transform.parent = Instance.transform;
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static DamageText GetDamageText(GameObject parent, Vector2 pos, string content)
    {
        if (Instance.MonsterQueue.Count > 0)
        {
            var obj = Instance.DamageTextQueue.Dequeue();
            obj.transform.SetParent(parent.transform);
            obj.transform.position = pos;
            obj.text.text = content;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewDamageText();
            newObj.transform.SetParent(parent.transform);
            newObj.transform.position = pos;
            newObj.text.text = content;
            newObj.gameObject.SetActive(true);
            return newObj;



        }
    }

    public static void ReturnDamageText(DamageText Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.DamageTextQueue.Enqueue(Obj);
    }


    #endregion
}

