using System;
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
    }

    private void OnEnable()
    {
    }

    public void SetButtonProbs(SkeletonUI skeleton, Sprite buttonImage)
    {
        this.skeletonTypeText.text = skeleton.skeletonName;
        this.skeletonDescriptionText.text = skeleton.skeletonDescribe;
        this.IconImage.sprite = skeleton.icon;
        skeletonObject = skeleton.prefab;
        GetComponent<Button>().image.sprite = buttonImage;
        GetComponent<Button>().onClick.AddListener(SpawnObject);
        GetComponent<Button>().onClick.AddListener(() => skeleton.AddUpgradeLimit());
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void SpawnObject()
    {
        SpawnManager.s_Instance.SpawnInCamera(skeletonObject);
    }
}
