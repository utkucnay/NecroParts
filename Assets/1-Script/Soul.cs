using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : CollectableMono
{
    public override void Collect()
    {
        SoulManager.s_Instance.AddSoul();
    }
}