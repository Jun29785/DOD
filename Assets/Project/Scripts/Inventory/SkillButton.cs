using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    private int SKey;

    [SerializeField]
    private Text buttonText;

    public void SetButton(int skey)
    {
        this.SKey = skey;

        string nameKey = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Name;

        Debug.Log("name : " + nameKey);

        buttonText.text = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Name;
    }

    public void OnClick()
    {
        List<int> decCommand = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Command;
        int decFmana = SkillDataBaseManager.Instance.tdSkillDict[this.SKey].Fmana;

        Debug.Log("Command"+ decCommand+ "FMana"+ decFmana);
    }
}
