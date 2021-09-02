using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillButton : MonoBehaviour
{
    private int SKey;
    private string Name;
    private List<int> Command;
    private int Mana;
    public GameObject SkillPanel;

    public void SetButton(int skey)
    {
        this.SKey = skey;

        string nameKey = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Name;

        Debug.Log("name : " + nameKey);

    }

    public void OnClick()
    {
        this.Name = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Name;
        this.Command = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Command;
        this.Mana = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Fmana + SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Lmana /* * PlayerLevel */;
        SkillPanel.SetActive(true);
    }
}
