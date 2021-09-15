using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.DB;
using DOD.Define;
public class GameManager : Singleton<GameManager>
{

    public TitleController titleController;
    public bool isnameInput;
    
    
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
            var i = UserDataManager.user.skill_level[item.Name];
            Debug.Log(item.Name);
            Debug.Log("skill_level : " + UserDataManager.user.skill_level[item.Name]);

            item.Tdmg =item.Fdmg +(float)(item.Ldmg * (i-1));
            item.Tmana = item.Fmana +(float)(item.Lmana * (i-1));
            item.T_Ctime = item.Ctime - (float)(item.L_Ctime * (i-1));

            if(item.T_Ctime < item.M_Ctime)
            {
                item.T_Ctime = item.M_Ctime;
            }
        }
    }

    
    
}

