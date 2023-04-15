using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Unit
{
    protected override void OnEnable()
    {
        maxHealth = GetComponent<SkeletonSM>().sSMConfig.skeletonProp.maxHealth;
        base.OnEnable();
        AIManager.s_Instance.RegisterSkeleton(this);
    }

    private void OnDisable()
    {
        AIManager.s_Instance.UnregisterSkeleton(this);  
    }

    private void Update()
    {
        Damage(1 * Time.deltaTime);

        if (!Utils.InMainCamera(transform.position, -1f))
        {
            Damage(float.MaxValue); 
        }
    }

    protected override void Death()
    {
        gameObject.SetActive(false);
        AIManager.s_Instance.DeathSkeleton(this);


        var go = Instantiate(AIManager.s_Instance.puffVFX);
        go.transform.position = transform.position;
        go.SetActive(true);
    }

}
