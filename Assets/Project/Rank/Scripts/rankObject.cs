using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rankObject : MonoBehaviour
{
    GameObject Contents;
    public Text scoreText;
    public Text rankText;
    public Text nicknameText;

    //임시
    public void setData(string rank, string Name, string Score)
    {
        rankText.text = rank.ToString();
        nicknameText.text = Name;
        scoreText.text = Score.ToString();
    }
}
