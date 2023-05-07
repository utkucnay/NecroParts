using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public float damage;
    bool lockMove = false;
    private float damageTime;

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

        OnDamage();
    }

    private void OnDamage()
    {
        if (damageTime > Time.time)
        {
            lockMove = true;
            material.SetFloat("_LerpDamage", Mathf.Clamp(Utils.Scale(1.25f, 1.15f, 0, .7f, damageTime - Time.time), 0, .7f));
            MoveDir(Vector2.zero);
        }
        else
        {
            lockMove = false;
            material.SetFloat("_LerpDamage", Mathf.Clamp(Utils.Scale(0f, .1f, 0.7f, 0, Time.time - damageTime), 0, .7f));
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
        damageTime = Time.time + 1.25f;
    }

    bool CheckDistancePlayer(float minDist)
    {
        return Vector3.Distance(transform.position, Player.s_Instance.transform.position) <= minDist;
    }
    protected override void Death()
    {
        Destroy(gameObject);
        AIManager.s_Instance.SpawnSkeleton(transform.position);
    }
}
