using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;



namespace DOD.DB
{
    public class SkillDataBaseManager : Singleton<SkillDataBaseManager>
    {
        public static Dictionary<int, TDSkill> tdSkillDict = new Dictionary<int, TDSkill>();

        public GameObject SkillButtonObj;

        public Transform SkillObjParent;

        protected override void Awake()
        {
            base.Awake();
            LoadSkillTable();
            Debug.Log("Successful Load Skill Table");
            CreateButton();
            Debug.Log("Successful Create Button");
        }

        void CreateButton()
        {
            foreach (var i in tdSkillDict)
            {
                Debug.Log("In");
                GameObject Create = (GameObject)Instantiate(SkillButtonObj);
                Create.transform.parent = SkillObjParent;
                Create.transform.localScale = new Vector3(1, 1, 1);
                Create.GetComponent<SkillButton>().SetButton(i.Value.SKey);
                Debug.Log("Create : " + Create.name);
            }
        }

        void LoadSkillTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Skill_Json"); // Json 불러오기

            tdSkillDict.Clear();

            JObject parsedObj = new JObject(); // Json Object 생성

            parsedObj = JObject.Parse(jsonText.text); // Json Parsing

            foreach (KeyValuePair<string, JToken> pair in parsedObj)
            {
                TDSkill tdSkill = new TDSkill();

                tdSkill.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
    
                tdSkillDict.Add(tdSkill.SKey, tdSkill);
            }
        }
    }
}