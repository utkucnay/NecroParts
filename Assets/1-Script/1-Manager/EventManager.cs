using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static Dictionary<string, UnityEvent> Events;

    public static void Init()
    {
        Events = new Dictionary<string, UnityEvent>();
    }

    public static void AddEventAction(string eventName, UnityAction action)
    {
        Events[eventName].AddListener(action);
    }

    public static void RemoveEventAction(string eventName, UnityAction action)
    {
        Events[eventName].RemoveListener(action);
    }

    public static void AddEvent(string eventName)
    {
        Events.Add(eventName, new UnityEvent());
    }

    public static void RemoveEvent(string eventName)
    {
        Events.Remove(eventName);
    }

    public static void InvokeEvent(string eventName)
    {
        Debug.Log(eventName);
        Events[eventName].Invoke();
    }
}
