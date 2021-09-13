using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.DB;
using DOD.Define;
public class GameManager : Singleton<GameManager>
{

    public TitleController titleController;
   
    
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    

    private void Start()
    {
        titleController.Initialize();
    }


    /// <summary>
    /// 앱세팅
    /// </summary>
    public void ApplicationSetting()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;
    }
    
    public void StatSetting()
    {
        foreach (var item in DataBaseManager.Instance.tdSkillDict.Values)
        {
            Debug.Log(item.Name);
            Debug.Log("skill_level : " + UserDataManager.user.skill_level[item.Name]);

            item.Fdmg += (int)(item.Ldmg * (UserDataManager.user.skill_level[item.Name]-1));
            item.Fmana += (int)(item.Lmana * (UserDataManager.user.skill_level[item.Name]-1));
        }
    }

    
    
}

