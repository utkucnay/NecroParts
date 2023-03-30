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
        Vector2 dir = Player.s_Instance.transform.position - transform.position;
        dir = dir.normalized;
        MoveDir(dir * 3);
    }
}
