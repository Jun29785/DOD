using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDUserRank : TableBase
{
    public string user_id;
    public string nickName;
    public string Score;



    public override void SetJsonData(string key, JObject info)
    {
       this.user_id = info["user_id"].Value<string>();
       this.nickName = info["nickname"].Value<string>();
       this.Score = info["score"].Value<string>();

    }
}

