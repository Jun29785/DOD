using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectpool : MonoBehaviour
{
    public static Objectpool Instance;

    [SerializeField]
    private GameObject CoinPrefabs;
    [SerializeField]
    private GameObject GoblinPrefabs;
    [SerializeField]
  ///  private Text DamageText;

    private Queue<Coin>CoinQueue = new Queue<Coin>();
    private Queue<Monster> GoblinQueue = new Queue<Monster>();
   // private Queue<Text> DamageTextQueue = new Queue<Text>();
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
        //    DamageTextQueue.Enqueue(CreateNewDamageText());
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
        if (Instance.GoblinQueue.Count > 0)
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

    #region DamageText

    /*private Text CreateNewDamageText()
    {
        var newObj = Instantiate(GoblinPrefabs).GetComponent<Text>();
        newObj.gameObject.SetActive(false);
        return newObj;

    }



    public static Text GetDamageText(Vector2 pos)
    {
        if (Instance.GoblinQueue.Count > 0)
        {
            var obj = Instance.DamageTextQueue.Dequeue();
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

    public static void ReturnDamageText(Text Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.DamageTextQueue.Enqueue(Obj);
    }
*/

    #endregion
}

