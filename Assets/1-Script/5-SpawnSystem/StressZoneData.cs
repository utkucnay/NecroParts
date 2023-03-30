using UnityEngine;

[System.Serializable]
public struct EnemyPrefab
{
    public GameObject prefab;
    public int rarity;
}

[System.Serializable]
public struct BossPrefab
{
    public GameObject prefab;
}

[System.Serializable]
public struct StressZone
{
    public EnemyPrefab[] enemyPrefabs;
    public BossPrefab[] bossPrefabs;
    public float timeSpent;
    public int minSpawnCount;
    public int maxSpawnCount;

    public float minSpawnWaitTime;
    public float maxSpawnWaitTime;
}

[CreateAssetMenu(fileName = "GameStress", menuName = "Spawn/GameStress", order = 1)]
public class StressZoneData : ScriptableObject
{
    public StressZone[] stressZones;
}
