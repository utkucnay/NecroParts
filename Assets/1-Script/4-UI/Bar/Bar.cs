using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar<T> : Singleton<T> where T : MonoBehaviour
{
    Slider slider;

    public override void Awake()
    {
        base.Awake();
        slider = GetComponent<Slider>();
    }

    public void UpdateSlider(float value, float maxValue)
    {
        slider.value = Utils.Scale(0, maxValue, 0.042f, 1, value);
    }
}
