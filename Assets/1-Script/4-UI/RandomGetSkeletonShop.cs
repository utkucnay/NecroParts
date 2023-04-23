using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGetSkeletonShop : MonoBehaviour
{
    public SkeletonUIData skeletonData;

    SkeletonButtons[] skeletonShopButtons;


    void Awake()
    {
        skeletonShopButtons = GetComponentsInChildren<SkeletonButtons>();

        foreach (var button in skeletonShopButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        int length = skeletonShopButtons.Length;
        List<SkeletonUI> skeletons = new List<SkeletonUI>(skeletonData.skeletons);

        for (int i = 0; i < length; i++)
        {
            var rNumb = Random.Range(0, skeletons.Count);
            var skeleton = skeletons[rNumb];
            if (skeleton.upgradeCount < (int)skeleton.upgradeLimit)
            {
                var rNumbSS = Random.Range(1, 101);

                if (rNumbSS <= 50)
                {
                    var rNumb2 = Random.Range(0, skeleton.skeletonUpgradeUIs.Length);
                    var skeletonUpgradeUI = skeleton.skeletonUpgradeUIs[rNumb2];
                    skeletonShopButtons[i].SetButton(skeleton, skeletonUpgradeUI, skeletonData.GetButtonImage(skeletonUpgradeUI.upgradeRarity));
                    skeletonShopButtons[i].gameObject.SetActive(true);
                    skeletons.Remove(skeleton);
                }

                else
                {
                    skeletonShopButtons[i].SetButton(skeleton, skeletonData.GetButtonImage(skeleton.skeletonType));
                    skeletonShopButtons[i].gameObject.SetActive(true);
                    skeletons.Remove(skeleton);
                }
            }
            else
            {
                skeletonShopButtons[i].SetButton(skeleton, skeletonData.GetButtonImage(skeleton.skeletonType));
                skeletonShopButtons[i].gameObject.SetActive(true);
                skeletons.Remove(skeleton);
            }
            
        }
    }
}
