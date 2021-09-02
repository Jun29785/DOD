using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redSlime : MeleeMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData(20001);
    }
}
