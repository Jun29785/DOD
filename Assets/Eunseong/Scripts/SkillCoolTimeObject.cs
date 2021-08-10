using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeObject : MonoBehaviour
{
    public Image image;
    public Image filledPanel;
    public Text text;

    public string skillName;
    public float CoolTime;
    public float CurrentTime;
    
    void Start()
    {
        CurrentTime = CoolTime;
    }

    void Update()
    {
        if(CurrentTime <= 0)
        {
            Destroy(gameObject);
        }

        text.text = $"{skillName}\n{CurrentTime.ToString("N1")}"; 
        CurrentTime -= Time.deltaTime;
        filledPanel.fillAmount = CurrentTime / CoolTime;
    }

    
}
