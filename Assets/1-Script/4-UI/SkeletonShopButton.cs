using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonShopButton : MonoBehaviour
{
    public TextMeshProUGUI skeletonTypeText;
    public TextMeshProUGUI skeletonDescriptionText;
    public Image IconImage;
    Sprite buttonImage;

    GameObject skeletonObject;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpawnObject);
    }

    private void OnEnable()
    {
    }

    public void SetButtonProbs(SkeletonShop skeleton, Sprite buttonImage)
    {
        this.skeletonTypeText.text = skeleton.skeletonName;
        this.skeletonDescriptionText.text = skeleton.skeletonDescribe;
        this.IconImage.sprite = skeleton.icon;
        skeletonObject = skeleton.prefab;
        GetComponent<Button>().image.sprite = buttonImage;
    }

    private void SpawnObject()
    {
        SpawnManager.s_Instance.SpawnInCamera(skeletonObject);
    }
}
