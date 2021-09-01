using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.DB;
public class rankController : MonoBehaviour
{

    public GameObject rankObj;
    public Transform rankObjParent;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void setRank()
    {
        DataBaseManager.Instance.LoadRankData();
        for (int i = 0; i < rankObjParent.childCount; i++)
        { 
            Destroy(rankObjParent.GetChild(i).gameObject); 
        }


        for (int i = 0; i < DataBaseManager.userRankDict.Count; i++)
        {
            var obj = Instantiate(rankObj);
            obj.transform.parent = rankObjParent.transform;
            obj.GetComponent<rankObject>().setData((i+1).ToString(),DataBaseManager.userRankDict[i].nickName,DataBaseManager.userRankDict[i].Score);

        }
    }
}
