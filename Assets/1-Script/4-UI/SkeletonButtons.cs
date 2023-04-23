using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonButtons : MonoBehaviour
{
    [SerializeField] SkeletonShopButton skeletonShopButton;
    [SerializeField] SkeletonUpgradeButton skeletonUpgradeButton;

    public void SetButton(SkeletonUI skeletonUI, Sprite sprite)
    {
        skeletonShopButton.gameObject.SetActive(true);
        skeletonShopButton.SetButtonProbs(skeletonUI, sprite);
    }

    public void SetButton(SkeletonUI skeleton, SkeletonUpgradeUI skeletonUpgradeUI, Sprite sprite)
    {
        skeletonUpgradeButton.gameObject.SetActive(true);
        skeletonUpgradeButton.SetButtonProbs(skeleton, skeletonUpgradeUI, sprite);
    }

    private void OnDisable()
    {
        skeletonShopButton.gameObject.SetActive(false);
        skeletonUpgradeButton.gameObject.SetActive(false);
    }
}
