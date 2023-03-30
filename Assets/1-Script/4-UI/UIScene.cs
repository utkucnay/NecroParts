using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScene : MonoBehaviour
{
    [SerializeField] string UIName;

    public void AddUI()
    {
        UIManager.s_Instance.AddUIKey(UIName, this);   
    }
}
