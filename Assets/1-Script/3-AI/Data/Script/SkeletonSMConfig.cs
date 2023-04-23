using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMultipler
{
    public float damageMultipler;
    public float attackRangeMultipler;
    public float attackSpeedMultipler;
    public float lifeMultipler;

    public static SkeletonMultipler Init()
    {
        return new SkeletonMultipler() 
        { 
            attackRangeMultipler = 1,
            attackSpeedMultipler = 1,
            lifeMultipler = 1,
            damageMultipler = 1,
        };
    }
}

public class SMConfig : ScriptableObject
{
    
}

[CreateAssetMenu(fileName = "Skeleton", menuName = "AIData/Skeleton", order = 1)]
public class SkeletonSMConfig : SMConfig
{
    public AttackData attackData;
    public RandomMoveData randomMoveData;
    public SkeletonProp skeletonProp;
    public SkeletonMultipler skeletonMultipler = SkeletonMultipler.Init();
}
