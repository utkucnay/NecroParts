using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Unit
{
    protected override void OnEnable()
    {
        base.OnEnable();
        AIManager.s_Instance.RegisterSkeleton(this);
    }

    private void OnDisable()
    {
        AIManager.s_Instance.UnregisterSkeleton(this);  
    }

    private void Update()
    {
        Damage(10 * Time.deltaTime);
    }

    protected override void Death()
    {
        gameObject.SetActive(false);
        AIManager.s_Instance.DeathSkeleton(this);
    }

}
