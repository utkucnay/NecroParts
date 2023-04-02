using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private void OnEnable()
    {
        AIManager.s_Instance.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        AIManager.s_Instance.UnregisterEnemy(this);
    }

    private void Update()
    {
        if (CheckDistancePlayer(.6f))
        {
            Player.s_Instance.DamageMelee(10 * Time.deltaTime, new DamageMeleeData());
        }
        else
        {
            MoveUpdate();
        }
    }

    void MoveUpdate()
    {
        Vector2 dir = Player.s_Instance.transform.position - transform.position;
        dir = dir.normalized;
        MoveDir(dir * 3);
        RotateDir(dir);
    }

    bool CheckDistancePlayer(float minDist)
    {
        return Vector3.Distance(transform.position, Player.s_Instance.transform.position) <= minDist;
    }
    protected override void Death()
    {
        Destroy(gameObject);
        SoulManager.s_Instance.SoulSpawn(transform.position);
        AIManager.s_Instance.SpawnSkeleton(transform.position);
    }
}
