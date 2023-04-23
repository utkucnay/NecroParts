using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventHandler : MonoBehaviour
{
    [SerializeField] string eventName;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => EventManager.InvokeEvent(eventName));
    }
}
