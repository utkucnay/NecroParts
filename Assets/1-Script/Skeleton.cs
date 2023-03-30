using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Unit
{
    private void OnEnable()
    {
        AIManager.s_Instance.RegisterSkeleton(this);
    }

    private void OnDisable()
    {
        AIManager.s_Instance.UnregisterSkeleton(this);  
    }
}
