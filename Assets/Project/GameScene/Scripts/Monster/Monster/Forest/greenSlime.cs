using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenSlime : ProjectileMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData(20002);
    }
}
