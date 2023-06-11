using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTimer : Singleton<WaveTimer>
{
    public float timer;

    public override void Awake()
    {
        base.Awake();

        EventManager.AddEventAction("Start Run", () => enabled = true);
        EventManager.AddEventAction("End Run", () =>
        {
            enabled = false; 
            timer = 0;
        });
    }

    private void Update()
    {
        timer += Time.deltaTime;

    }
}
