using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.DB;
public class rankController : MonoBehaviour
{
    public GameObject RankObj_1;
    public GameObject RankObj_2;
    public GameObject RankObj_3;

    public GameObject normalRankObj;
    public Transform rankObjParent;

    public void setRank()
    {
        DataBaseManager.Instance.LoadRankData();
        for (int i = 0; i < rankObjParent.childCount; i++)
        { 
            Destroy(rankObjParent.GetChild(i).gameObject); 
        }


        for (int i = 0; i < DataBaseManager.Instance.userRankDict.Count; i++)
        {
            GameObject obj;
            switch (i+1)
            {
                case 1:
                    obj = Instantiate(RankObj_1);
                    break;
                case 2:
                    obj = Instantiate(RankObj_2);

                    break;
                case 3:
                    obj = Instantiate(RankObj_3);

                    break;
                default:
                    obj = Instantiate(normalRankObj);

                    break;
            }
            obj.transform.parent = rankObjParent.transform;
            obj.GetComponent<rankObject>().setData((i+1).ToString(),DataBaseManager.Instance.userRankDict[i].nickName,DataBaseManager.Instance.userRankDict[i].Score);

        }
    }
}
