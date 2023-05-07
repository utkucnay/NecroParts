using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGraveyard : MonoBehaviour
{
    public GameObject graveyardPrefab;
    public GameObject[] graveyards;
    public int graveyardCount;

    private void Awake()
    {
        graveyards = new GameObject[graveyardCount];
        EventManager.AddEventAction("Start Run",() => SpawnGraveyards(graveyardCount));
    }

    void SpawnGraveyards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var graveyard = SpawnManager.s_Instance.SpawnOutCamera(graveyardPrefab);
            graveyard.transform.parent = transform;
            graveyards[i] = graveyard;
        }
    }


}
