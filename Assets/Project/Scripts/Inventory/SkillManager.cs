using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.DB;
public class SkillManager : Singleton<SkillManager>
{
    GameObject DBManager;

    DataBaseManager _DBManager;

    public GameObject SkillButtonObj;
    public Transform SkillObjParent;

    void Awake()
    {
        base.Awake();
        if (DBManager == null)
        {
            DBManager = GameObject.FindGameObjectWithTag("DataBase");
            
        }
    }


   
    
    void CreateButton()
    {
        foreach (var i in _DBManager.tdSkillDict)
        {
            Debug.Log("In");
            GameObject Create = (GameObject)Instantiate(SkillButtonObj);
            Create.transform.parent = SkillObjParent;
            Create.transform.localScale = new Vector3(1, 1, 1);
            Create.GetComponent<SkillButton>().SetButton(i.Value.SKey);
            Debug.Log("Create : " + Create.name);
        }
    }
}
