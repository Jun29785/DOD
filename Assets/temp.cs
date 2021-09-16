using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temp : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UserDataManager.Instance.isexist)
        {
            text.text = "True";
        }
        else
        {
            text.text = "False";
        }
    }
}
