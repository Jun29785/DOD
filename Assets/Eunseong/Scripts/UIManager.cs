using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Character character;
    public Slider Hp;
    void Start()
    {
        Hp.maxValue = character.MaxHp;
        
    }

    // Update is called once per frame
    void Update()
    {
        Hp.value = character.Hp;
    }
}
