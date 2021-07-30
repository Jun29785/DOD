using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    int gold = 0;
    
    public void GetGold(int value)
    {
        gold += value;
    }

}
