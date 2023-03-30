using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonChangeScene : MonoBehaviour
{
    [SerializeField] string UIName;
    [SerializeField] bool allMenuClose = true;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UIManager.s_Instance.SetUIScene(UIName, allMenuClose));
    }
}
