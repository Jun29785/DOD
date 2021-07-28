using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectpool : MonoBehaviour
{
    public static Objectpool Instance;

    [SerializeField]
    private GameObject CoinPrefabs;
    [SerializeField]
    private GameObject GoblinPrefabs;

    private Queue<Coin>CoinQueue = new Queue<Coin>();
    private Queue<Monster> GoblinQueue = new Queue<Monster>();
    void Start()
    {
        Instance = this;
        Initialize(12);
    }
    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CoinQueue.Enqueue(CreateNewCoin());
            GoblinQueue.Enqueue(CreateNewGoblin());
        }
    }

    #region Coin

    private Coin CreateNewCoin()
    {
        var newObj = Instantiate(CoinPrefabs).GetComponent<Coin>();
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static Coin GetCoinobject(Vector2 pos)
    {
        if (Instance.CoinQueue.Count > 0)
        {
            var obj = Instance.CoinQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewCoin();
            newObj.transform.SetParent(null);
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

    #region Goblin

    private Monster CreateNewGoblin()
    {
        var newObj = Instantiate(GoblinPrefabs).GetComponent<Monster>();
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static Monster GetGoblinobject(Vector2 pos)
    {
        if (Instance.CoinQueue.Count > 0)
        {
            var obj = Instance.GoblinQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewGoblin();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnGoblin(Monster Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.GoblinQueue.Enqueue(Obj);
    }

    #endregion
}

