using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public class AIManager : Singleton<AIManager>
{
    public const int MAX_AI_AND_ENEMiES = 200;

    LinkedList<Enemy> Enemies;
    LinkedList<Skeleton> skeletons;

    Queue<Skeleton> deathSkeletons;

    public LinkedList<int> priorities;

    [SerializeField] public GameObject puffVFX;
    [SerializeField] public GameObject bones;

    public override void Awake()
    {
        base.Awake();
        Enemies = new LinkedList<Enemy>();
        skeletons = new LinkedList<Skeleton>();
        priorities = new LinkedList<int>();
        deathSkeletons = new();
    }

    public int GetPriortySkeleton()
    {
        var last = priorities.Last;
        priorities.RemoveLast();
        return last.Value;
    }

    public void SetPriortySkeleton(int value)
    {
        priorities.AddLast(value);
    }

    public int GetPriortyEnemy()
    {
        var last = priorities.First;
        priorities.RemoveFirst();
        return last.Value;
    }

    public void SetPriortyEnemy(int value)
    {
        priorities.AddFirst(value);
    }

    public void RegisterEnemy(Enemy enemy)
    {
        Enemies.AddLast(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

    public LinkedList<Enemy> GetEnemies()
    {
        return Enemies;
    }

    public Enemy[] GetCloseEnemies(int distance, Vector2 pos)
    {
        return Enemies.Where(enemy => Vector2.Distance( enemy.transform.position, pos) <= distance).ToArray();
    }

    public Enemy GetEnemy(int index)
    {
        return Enemies.ElementAt(index);
    }

    public void RegisterSkeleton(Skeleton skeleton)
    {
        skeletons.AddLast(skeleton);
    }

    public void UnregisterSkeleton(Skeleton skeleton)
    {
        skeletons.Remove(skeleton);
    }

    public void DeathSkeleton(Skeleton skeleton)
    {
        deathSkeletons.Enqueue(skeleton);
    }

    public void SpawnSkeleton(Vector3 pos)
    {
        if (deathSkeletons.Count <= 0) 
        {
            var treshBone = Instantiate(bones);
            treshBone.gameObject.transform.position = pos;
            treshBone.gameObject.SetActive(true);
        }
        else
        {
            var skeleton = deathSkeletons.Dequeue();
            skeleton.gameObject.transform.position = pos;
            skeleton.gameObject.SetActive(true);
        }
    }

    public LinkedList<Skeleton> GetSkeletons()
    {
        return skeletons;
    }

    public Skeleton GetSkeletons(int index)
    {
        return skeletons.ElementAt(index);
    }

    public int[] CalculateTargetPointsForPos(in Vector2 pos, float maxDistance, float scale)
    {
        List<int> targetPoints = new List<int>(Enemies.Count);

        foreach (var enemy in GetEnemies())
        {
            var dist = Vector2.Distance(enemy.transform.position, pos);
            if (dist < maxDistance)
            {
                targetPoints.Add((int)Utils.Scale(0, 9, scale, 0, dist));
            }
            else
            {
                targetPoints.Add(0);
            }
        }

        return targetPoints.ToArray();
    }

    public void DestroyAllObject()
    {
        int lenght = Enemies.Count;
        for (int i = 0; i < lenght; i++)
        {
            var enemy = Enemies.First;
            Enemies.RemoveFirst();
            Destroy(enemy.Value.gameObject);
        }

        lenght = skeletons.Count;
        for (int i = 0; i < lenght; i++)
        {
            var skeleton = skeletons.First;
            skeletons.RemoveFirst();
            Destroy(skeleton.Value.gameObject);
        }
    }
}