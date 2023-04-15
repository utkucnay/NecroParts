using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkeletonShop
{
    public enum SkeletonType
    {
        Melee,
        Ranged,
        Tank,
        Support
    }

    public string skeletonName;
    public string skeletonDescribe;
    public Sprite icon;
    public SkeletonType skeletonType;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "SkeletonData", menuName = "CardData/Skeleton", order = 1)]
public class SkeletonsShopData : ScriptableObject
{
    public SkeletonShop[] skeletons;

    public Sprite meleeButtonImage;
    public Sprite rangedButtonImage;
    public Sprite tankButtonImage;
    public Sprite supportButtonImage;

    public Sprite GetButtonImage(SkeletonShop.SkeletonType skeletonType)
    {
        switch (skeletonType)
        {
            case SkeletonShop.SkeletonType.Melee:
                return meleeButtonImage;
            case SkeletonShop.SkeletonType.Ranged:
                return rangedButtonImage;
            case SkeletonShop.SkeletonType.Tank:
                return tankButtonImage;
            case SkeletonShop.SkeletonType.Support:
                return supportButtonImage;
            default:
                break;
        }
        return null;
    }
}