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

    public LinkedList<int> priorities;

    public override void Awake()
    {
        base.Awake();
        Enemies = new LinkedList<Enemy>();
        skeletons = new LinkedList<Skeleton>();
        priorities = new LinkedList<int>();
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
}