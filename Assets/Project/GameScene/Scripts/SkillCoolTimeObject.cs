using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillCoolTimeObject : MonoBehaviour
{
    public Image image;
    public Image filledPanel;
    public Text text;
    public RectTransform RT;
    public int skillKey;
    public string skillName;
    public float CoolTime;
    public float CurrentTime;
    
    void Start()
    {
        Debug.Log((1280 / Screen.height));
        RT = GetComponent<RectTransform>();

        Debug.Log(Camera.main.rect.height);
        Debug.Log(Screen.height);
        Debug.Log(Screen.height * Camera.main.rect.height);
        Debug.Log(( (Screen.height * Camera.main.rect.height / (float)1280 )));
        Debug.Log(RT.sizeDelta.x * ((Screen.height * Camera.main.rect.height)/ 1280));

        RT.sizeDelta = new Vector2(RT.sizeDelta.x * ( (Screen.height * Camera.main.rect.height) / (float)1280), RT.sizeDelta.y * ( (Screen.height * Camera.main.rect.height)/(float)1280));
        skillName = DataBaseManager.Instance.tdSkillDict[skillKey].Name;
        CoolTime = DataBaseManager.Instance.tdSkillDict[skillKey].T_Ctime;

        if (Resources.Load<Sprite>("SkillIcon/" + skillKey) != null)
        {
            image.sprite = Resources.Load<Sprite>("SkillIcon/" + skillKey) as Sprite;
        }
        
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
