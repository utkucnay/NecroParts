using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGetSkeeltonShop : MonoBehaviour
{
    public SkeletonsShopData skeletonData;

    SkeletonShopButton[] skeletonShopButtons;


    void Awake()
    {
        skeletonShopButtons = GetComponentsInChildren<SkeletonShopButton>();

        foreach (var button in skeletonShopButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        int length = skeletonShopButtons.Length;
        List<SkeletonShop> skeletons = new List<SkeletonShop>(skeletonData.skeletons);

        for (int i = 0; i < length; i++)
        {
            var rNumb = Random.Range(0, skeletons.Count);
            var skeleton = skeletons[rNumb];
            skeletonShopButtons[i].SetButtonProbs(skeleton, skeletonData.GetButtonImage(skeleton.skeletonType));
            skeletonShopButtons[i].gameObject.SetActive(true);
            skeletons.Remove(skeleton);
        }
    }
}
