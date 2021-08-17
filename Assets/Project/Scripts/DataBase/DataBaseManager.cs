using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

namespace DB
{

    public class DataBaseManager : Singleton<DataBaseManager>
    {
        public Dictionary<int, TDMonster> tdMonsterDict = new Dictionary<int, TDMonster>();

        protected override void Awake()
        {
            base.Awake();
            LoadMonsterTable();
        }


        void LoadMonsterTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Monster_Json"); // Json 불러오기  

            tdMonsterDict.Clear();

            JObject parsedObj = new JObject(); //Json Object 생성

            parsedObj = JObject.Parse(jsonText.text);  //제이슨 파싱


            foreach (KeyValuePair<string, JToken> pair in parsedObj)
            {
                TDMonster tdMonster = new TDMonster();

                tdMonster.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                tdMonsterDict.Add(tdMonster.unitNo, tdMonster);
            }

        }
    }
}