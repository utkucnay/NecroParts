using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    MoveCommandTransform moveCommandTransform;
    RotateCommand rotateCommand;

    [SerializeField] protected float maxHealth;
    protected float health;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        moveCommandTransform = new MoveCommandTransform(transform, Vector2.zero);
        rotateCommand = new RotateCommand(spriteRenderer, Vector2.zero);
    }

    protected virtual void Start()
    {
        health = maxHealth;
    }

    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    public void MoveDir(Vector2 velocity)
    {
        moveCommandTransform._velocity = velocity;
        CommandStream.AddCommand(moveCommandTransform);
    }

    public void RotateDir(Vector2 dir)
    {
        rotateCommand._dir = dir;
        CommandStream.AddCommand(rotateCommand);
    }

    public void DamageMelee(float damage, DamageMeleeData damageMeleeData)
    {
        Damage(damage);
    }

    protected virtual void Damage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            Death();
        }
    }

    protected abstract void Death();
}


public struct DamageMeleeData
{
    public Vector3 meleePos;
}