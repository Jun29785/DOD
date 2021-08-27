using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MultiMap<TKey, TValue>
{
    private readonly Dictionary<TKey, IList<TValue>> storage;

    public MultiMap()
    {
        storage = new Dictionary<TKey, IList<TValue>>();
    }

    public void Add(TKey key, TValue value)
    {
        if (!storage.ContainsKey(key)) storage.Add(key, new List<TValue>());
        storage[key].Add(value);
    }

    public IEnumerable<TKey> Keys
    {
        get { return storage.Keys; }
    }

    public bool ContainsKey(TKey key)
    {
        return storage.ContainsKey(key);
    }

    public IList<TValue> this[TKey key]
    {
        get
        {
            if (!storage.ContainsKey(key))
                throw new KeyNotFoundException(
                    string.Format(
                        "The given key {0} was not found in the collection.", key));
            return storage[key];
        }
    }

    public TValue Removeit(TKey unit)
    {
        var obj = storage[unit][0];
        storage[unit].RemoveAt(0);

        return obj;
    }
}
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

    MultiMap<int,Monster> Monstermap = new MultiMap<int, Monster>();

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

        for (int i = 0; i < 1000; i++)
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
            Monstermap.Add(newObj.unitNo, newObj);
        }
        

    }



    public static Monster GetMonsterobject(int monsterNo,  Vector2 pos/*, BattleManager.Monster type*/)
    {
        if (Instance.Monstermap[monsterNo].Count > 0)
        {
            var obj = Instance.Monstermap.Removeit(monsterNo);
            
            obj.transform.position = pos;
            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            Instance.CreateNewMonsters();
            var newObj = Instance.Monstermap.Removeit(monsterNo);

            newObj.transform.position = pos;
            newObj.gameObject.SetActive(true);

            return newObj;
        }
    }

    public static void ReturnMonster(Monster Obj)
    {
        Obj.gameObject.SetActive(false);
        Obj.transform.SetParent(Instance.transform);
        Instance.Monstermap.Add(Obj.unitNo,Obj);
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
        if (Instance.DamageTextQueue.Count > 0)
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

