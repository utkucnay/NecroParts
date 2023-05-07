using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SkeletonUI
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
    public SkeletonSMConfig skeletonSMConfig;

    public string upgradeLimitText { get { StringBuilder sb = new(); return sb.Append(upgradeCount).Append("/").Append((int)upgradeLimit).ToString(); } }
    [HideInInspector] public int upgradeCount;
    [HideInInspector] public float upgradeLimit;
    [HideInInspector] public SkeletonUpgradeUI[] skeletonUpgradeUIs;

    public void AddUpgradeLimit()
    {
        upgradeLimit += 1.5f;
    }

    public void AddUpgradeCount()
    {
        upgradeCount++;
    }

    public void Reset()
    {
        upgradeCount = 0; 
        upgradeLimit = 0;
        skeletonUpgradeUIs = null;

    }

    public void Setup(List<SkeletonUpgradeUI> skeletonUpgradeUIs)
    {
        this.skeletonUpgradeUIs = skeletonUpgradeUIs.ToArray();
    }
    public GameObject prefab;
}

[System.Serializable]
public class SkeletonUpgradeUI
{
    public enum UpgradeType
    {
        Damage,
        AttackRange,
        AttackSpeed,
        Life
    }

    public enum UpgradeRarity
    {
        Uncommon
    }

    public string skeletonName;
    public string upgradeName;
    public string upgradeDescribe;
    public float upgradeValue;

    public UpgradeType upgradeType;
    public UpgradeRarity upgradeRarity;

    [HideInInspector] public UnityEvent OnClick;

    public void AddOnClickEvent(UnityAction action)
    {
        OnClick.AddListener(action);
    }
}

[CreateAssetMenu(fileName = "SkeletonData", menuName = "CardData/Skeleton", order = 1)]
public class SkeletonUIData : ScriptableObject
{
    public SkeletonUI[] skeletons;
    public SkeletonUpgradeUI[] skeletonUpgrades;

    [Header("Skeleton UI")]
    public Sprite meleeButtonImage;
    public Sprite rangedButtonImage;
    public Sprite tankButtonImage;
    public Sprite supportButtonImage;

    [Header("Upgrade UI")]
    public Sprite UncommonButtonImage;
    public Sprite rareButtonImage;
    public Sprite epicButtonImage;
    public Sprite legandaryButtonImage;

    public static SkeletonUIData instance;

    public SkeletonUIData()
    {
        instance = this;
    }

    void Awake()
    {
    }

    public Sprite GetButtonImage(SkeletonUI.SkeletonType skeletonType)
    {
        switch (skeletonType)
        {
            case SkeletonUI.SkeletonType.Melee:
                return meleeButtonImage;
            case SkeletonUI.SkeletonType.Ranged:
                return rangedButtonImage;
            case SkeletonUI.SkeletonType.Tank:
                return tankButtonImage;
            case SkeletonUI.SkeletonType.Support:
                return supportButtonImage;
            default:
                break;
        }
        return null;
    }

    public Sprite GetButtonImage(SkeletonUpgradeUI.UpgradeRarity skeletonType)
    {
        switch (skeletonType)
        {
            case SkeletonUpgradeUI.UpgradeRarity.Uncommon:
                return UncommonButtonImage;
            default:
                break;
        }
        return null;
    }

    public void Setup()
    {
        foreach (var skeleton in skeletons)
        {
            List<SkeletonUpgradeUI> skeletonUpgradeUIs = new();
            foreach(var skeletonUpgrade in skeletonUpgrades)
            {
                if (skeleton.skeletonName.GetHashCode() == skeletonUpgrade.skeletonName.GetHashCode())
                {
                    skeletonUpgradeUIs.Add(skeletonUpgrade);

                    skeletonUpgrade.AddOnClickEvent(() =>
                    {
                        switch (skeletonUpgrade.upgradeType)
                        {
                            case SkeletonUpgradeUI.UpgradeType.Damage:
                                skeleton.skeletonSMConfig.skeletonMultipler.damageMultipler += skeletonUpgrade.upgradeValue;
                                break;
                            case SkeletonUpgradeUI.UpgradeType.AttackRange:
                                skeleton.skeletonSMConfig.skeletonMultipler.attackRangeMultipler += skeletonUpgrade.upgradeValue;
                                break;
                            case SkeletonUpgradeUI.UpgradeType.AttackSpeed:
                                skeleton.skeletonSMConfig.skeletonMultipler.attackSpeedMultipler += skeletonUpgrade.upgradeValue;
                                break;
                            case SkeletonUpgradeUI.UpgradeType.Life:
                                skeleton.skeletonSMConfig.skeletonMultipler.lifeMultipler += skeletonUpgrade.upgradeValue;
                                break;
                            default:
                                break;
                        }
                    });
                }
            }
            if (skeletonUpgradeUIs.Count <= 0) continue;
            skeleton.Setup(skeletonUpgradeUIs);
        }
    }

    public void ResetSekeleton()
    {
        foreach (var skeleton in skeletons)
        {
            skeleton.Reset();
        }
    }
}