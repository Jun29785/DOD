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

        private void Awake()
        {
            LoadMonsterTable();
        }


        void LoadMonsterTable()
        {
            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/Monster_Json"); // Json 불러오기  

            tdMonsterDict.Clear();

            JObject parsedObj = new JObject(); //Json Object 생성

            parsedObj = JObject.Parse(jsonText.text);  //제이슨 파싱

            Debug.Log(parsedObj);

            foreach (KeyValuePair<string, JToken> pair in parsedObj)
            {
                TDMonster tdMonster = new TDMonster();

                Debug.Log("key : " + pair.Key);
                Debug.Log("value : " + pair.Value);
                tdMonster.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                Debug.Log("Test1");
                tdMonsterDict.Add(tdMonster.unitNo, tdMonster);
            }

            Debug.Log("test1");
        }
    }
}