using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Dictionary<string, UIScene> UIs;

    public override void Awake()
    {
        base.Awake();
        UIs = new Dictionary<string, UIScene>();

        var UIObjs =  Resources.FindObjectsOfTypeAll<UIScene>();

        foreach (var UIObj in UIObjs)
        {
            UIObj.AddUI();
        }
    }

    private void Start()
    {
        
    }

    public void SetUIScene(string name, bool allMenuClose = false)
    {
        if (allMenuClose)
        {
            foreach (var UI in UIs.Values)
            {
                UI.gameObject.SetActive(false);
            }
        }
        if (UIs.ContainsKey(name))
            UIs[name].gameObject.SetActive(true);
        else
            Debug.LogWarning("Not Contains UI Key: " + name);
    }

    public void AddUIKey(string name, UIScene UIObject)
    {
        if (UIs.ContainsKey(name))
            Debug.LogWarning("Already Added UI Key: " + name);
        else
            UIs.Add(name, UIObject);
    }
}
