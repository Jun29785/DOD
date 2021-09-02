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
    public int Fmana;
    public float Lmana;
    public int Fdmg;
    public float Ldmg;
    public int Ctime;
    public string Description;
    public int SkillLevel;
    public Image SkillIcon;

    void Awake()
    {
        base.Awake();
        
    }
}
