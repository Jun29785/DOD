using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player;
    public Slider Hp;
    void Start()
    {
        Hp.maxValue = player.MaxHpP;
        
    }

    // Update is called once per frame
    void Update()
    {
        Hp.value = player.HpP;
    }
}
