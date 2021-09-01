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

    [SerializeField]
    private Text buttonText;

    public void SetButton(int skey)
    {
        this.SKey = skey;

        string nameKey = SkillDataBaseManager.tdSkillDict[this.SKey].Name;

        Debug.Log("name : " + nameKey);

        buttonText.text = SkillDataBaseManager.tdSkillDict[this.SKey].Name;
    }

    public void OnClick()
    {
        this.Name = SkillDataBaseManager.tdSkillDict[this.SKey].Name;
        this.Command = SkillDataBaseManager.tdSkillDict[this.SKey].Command;
        this.Mana = SkillDataBaseManager.tdSkillDict[this.SKey].Fmana + SkillDataBaseManager.tdSkillDict[this.SKey].Lmana /* * PlayerLevel */;
        SkillPanel.SetActive(true);
    }
}
