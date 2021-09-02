using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private int SKey;
    [SerializeField]
    private string Name;
    [SerializeField]
    private List<int> Command;
    [SerializeField]
    private float Mana;
    [SerializeField]
    private float Dmg;
    [SerializeField]
    private string Description;
    [SerializeField]
    private int SkillLevel = 0;
    [SerializeField]
    private Image SkillIcon;

    public GameObject SkillPanel;

    public void SetButton(int skey)
    {
        this.SKey = skey;

        string nameKey = DataBaseManager.Instance.tdSkillDict[this.SKey].Name;

        Debug.Log("name : " + nameKey);

    }

    private void Awake()
    {
        init();
    }

    public void init()
    {
        var i = SkillManager.Instance;
        this.Name = i.Name;
        this.Command = i.Command;
        this.Mana = i.Fmana + (i.Lmana*SkillLevel);
        this.Dmg = i.Fdmg + (i.Ldmg * SkillLevel);
        this.Description = i.Description;
        this.SkillIcon = i.SkillIcon;
    }
}
