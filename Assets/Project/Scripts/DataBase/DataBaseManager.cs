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
        public List<TDUserRank> userRankDict = new List<TDUserRank>();


        protected override void Awake()
        {
            base.Awake();
            LoadMonsterTable();
            LoadRankData();
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


        public void LoadRankData()
        {

            TextAsset jsonText = Resources.Load<TextAsset>("DataTable/UserDataJson"); // Json 불러오기  

            userRankDict.Clear();

            JObject parsedObj = new JObject(); //Json Object 생성

            parsedObj = JObject.Parse(jsonText.text);

            JArray jArray = new JArray();
            jArray = JArray.Parse(parsedObj["data"].ToString());

            foreach (JObject jo in jArray)
            {

                TDUserRank tdrank = new TDUserRank();

                tdrank.SetJsonData(jo["user_id"].Value<string>(), jo);


                userRankDict.Add(tdrank);
                //tdUser.SetJsonData(pair.Key, pair.Value.ToObject<JObject>());
                //userDict.Add(tdUser.nickName, tdUser);
            }


        }
    }
}