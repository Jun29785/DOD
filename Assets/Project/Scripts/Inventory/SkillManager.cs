using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillManager : Singleton<SkillManager>
{

    public List<int> Command;
    public int SKey;
    public string Name;
    public float Mana;
    public float Dmg;
    public int Ctime;
    public string Description;
    public int SkillLevel;
    public int UpgrateCost;
    public Image SkillIcon;

    void Awake()
    {
        base.Awake();
        
    }
}
