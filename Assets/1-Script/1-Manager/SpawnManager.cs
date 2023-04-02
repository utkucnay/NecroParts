using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] StressZoneData stressZoneData;

    StressZone stressZone;
    int index;
    float spendTime;
    float stressZoneTimer;

    public override void Awake()
    {
        base.Awake();

        EventManager.AddEventAction("Start Run", 
            () => enabled = true);
        EventManager.AddEventAction("End Run",
            () => enabled = false);
    }

    private void OnEnable()
    {
        Debug.Log("Active Spawner");

        index = 0;
        stressZone = stressZoneData.stressZones[index];
        StartCoroutine(SpawnerLogic());
    }

    private void OnDisable()
    {
        Debug.Log("Inactive Spawner");
        StopAllCoroutines();
    }

    private void Start()
    {
    }

    void Update()
    {
        var enemies = AIManager.s_Instance.GetEnemies();

        foreach (var enemy in enemies)
        {
            Vector2 dir = enemy.transform.position - Player.s_Instance.transform.position;
            float magnitude = dir.magnitude;

            if (magnitude >= 20)
            {
                dir = dir.normalized;
                dir.Rotate(180);
                enemy.transform.position = Player.s_Instance.transform.position + GameManager.Translate3D(dir * 17);
            }
        }
    }

    IEnumerator SpawnerLogic()
    {
        while (true)
        {
            int spawnCount = Random.Range(stressZone.minSpawnCount, stressZone.maxSpawnCount + 1);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnOutCamera(CalculateRarity());
            }

            float waitTime = Utils.Scale(0, stressZone.timeSpent, stressZone.maxSpawnWaitTime, stressZone.minSpawnWaitTime, stressZoneTimer);

            yield return new WaitForSeconds(waitTime);

            stressZoneTimer += waitTime;

            if (WaveTimer.s_Instance.timer > spendTime + stressZone.timeSpent)
            {
                if (++index < stressZoneData.stressZones.Length)
                {
                    stressZoneTimer = 0;
                    spendTime += stressZone.timeSpent;
                    stressZone = stressZoneData.stressZones[index];

                    foreach (var boss in stressZone.bossPrefabs)
                    {
                        SpawnOutCamera(boss.prefab);
                    }
                }
                else
                {
                    yield break;
                }
            }
        }
    }

    public void SpawnInCamera(GameObject gameObject)
    {
        var dir = Vector2.up;
        dir.Rotate(Random.Range(0, 360));
        dir *= Random.Range(3, 9);
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        var position = dir + (Vector2)camera.transform.position;

        var obj = Instantiate(gameObject);
        obj.transform.position = GameManager.Translate3D(position);
    }

    public Vector3 PosInCamera()
    {
        var dir = Vector2.up;
        dir.Rotate(Random.Range(0, 360));
        dir *= Random.Range(3, 9);
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        return dir + (Vector2)camera.transform.position;
    }

    public void SpawnOutCamera(GameObject gameObject)
    {
        var dir = Vector2.up;
        dir.Rotate(Random.Range(0, 360));
        dir *= 19f;
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        var position = dir + (Vector2)camera.transform.position;

        var agent = Instantiate(gameObject);
        agent.transform.position = GameManager.Translate3D(position);
    }

    GameObject CalculateRarity()
    {
        int length = stressZone.enemyPrefabs.Length;
        int sumRarity = 0;
        for (int i = 0; i < length; i++)
        {
            sumRarity += stressZone.enemyPrefabs[i].rarity;
        }

        int rand = Random.Range(0, sumRarity);
        sumRarity = 0;
        for (int i = 0; i < length; i++)
        {
            sumRarity += stressZone.enemyPrefabs[i].rarity;
            if (rand < sumRarity)
            {
                return stressZone.enemyPrefabs[i].prefab;
            }
        }
        return null;
    }
}
