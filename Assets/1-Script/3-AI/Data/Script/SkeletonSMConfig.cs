using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skeleton", menuName = "AIData/Skeleton", order = 1)]
public class SkeletonSMConfig : ScriptableObject
{
    public AttackData attackData;
    public RandomMoveData randomMoveData;
    public SkeletonProp skeletonProp;
}
