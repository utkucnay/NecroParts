using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public float damage;
    bool sequacerStart = true;
    bool lockMove = false;
    protected override void OnEnable()
    {
        base.OnEnable();
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
            Player.s_Instance.DamageMelee(damage * Time.deltaTime, new DamageMeleeData());
        }
        else
        {
            if (!lockMove)
            {
                MoveUpdate();
            }
        }
    }

    void MoveUpdate()
    {
        Vector2 dir = Player.s_Instance.transform.position - transform.position;
        dir = dir.normalized;
        MoveDir(dir * 3);
        RotateDir(dir);
    }

    protected override void Damage(float damage)
    {
        base.Damage(damage);
        if (sequacerStart)
        {
            StartCoroutine(WaitTime(2));
        }

    }

    IEnumerator WaitTime(float time)
    {
        sequacerStart = false;
        lockMove = true;
        yield return new WaitForSeconds(time);
        lockMove = false;
        sequacerStart = true;
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
