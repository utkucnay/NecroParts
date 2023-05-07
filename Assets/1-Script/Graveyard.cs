using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    [SerializeField] private float thresholdDistance = 30;

    private void Update()
    {
        if(Vector2.Distance(Player.s_Instance.transform.position, transform.position) > thresholdDistance)
        {
            transform.position = SpawnManager.s_Instance.PosOutCamera();
        }
    }
}
