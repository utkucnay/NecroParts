using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonUpgradeButton : MonoBehaviour
{
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDiscription;
    public TextMeshProUGUI upgradeLimit;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
    }

    public void SetButtonProbs(SkeletonUI skeleton, SkeletonUpgradeUI skeletonUpgrade, Sprite buttonImage)
    {
        this.upgradeName.text = skeletonUpgrade.upgradeName;
        this.upgradeDiscription.text = skeletonUpgrade.upgradeDescribe;
        this.upgradeLimit.text = skeleton.upgradeLimitText;
        GetComponent<Button>().image.sprite = buttonImage;
        GetComponent<Button>().onClick.AddListener(skeletonUpgrade.OnClick.Invoke);
        GetComponent<Button>().onClick.AddListener(skeleton.AddUpgradeCount);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
