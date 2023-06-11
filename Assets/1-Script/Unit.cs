using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Unit : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    protected Material material;
    protected Animator animator;
    protected Rigidbody rb;

    MoveCommandTransform moveCommandTransform;
    RotateCommand rotateCommand;

    [SerializeField] protected float maxHealth;
    protected float health;

    protected bool death;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        material = spriteRenderer.material;
        moveCommandTransform = new MoveCommandTransform(transform, Vector2.zero);
        rotateCommand = new RotateCommand(spriteRenderer, Vector2.zero);
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    protected virtual void OnEnable()
    {
        health = maxHealth;
        death = false;
    }

    public void MoveDir(Vector2 velocity)
    {
        moveCommandTransform._velocity = velocity;
        CommandStream.AddCommand(moveCommandTransform);
        if(velocity == Vector2.zero) animator?.SetFloat("_TimeScale", 0);
        else animator?.SetFloat("_TimeScale", velocity.magnitude / 2);
    }

    public void RotateDir(Vector2 dir)
    {
        rotateCommand._dir = dir;
        CommandStream.AddCommand(rotateCommand);
    }

    public virtual void DamageMelee(float damage, DamageMeleeData damageMeleeData)
    {
        Damage(damage);
    }

    public virtual void DamageProjectile(float damage, DamageProjectileData damageMeleeData)
    {
        Damage(damage);
    }

    protected virtual void Damage(float damage)
    {
        health -= damage;

        if (health <= 0 && !death)
        {
            death = true;
            Death();
        }
    }

    protected virtual void Knockback(Vector3 dir, float knockbackPower) 
    {
        transform.DOMove(transform.position + dir * knockbackPower, .1f);     
    }

    protected virtual void Death()
    {
        DOTween.Kill(transform);
    }
}


public struct DamageMeleeData
{
    public Vector3 meleePos;
    public float knockbackPower;
}


public struct DamageProjectileData
{
    public Vector3 projectilePos;
    public Vector3 moveDir;
    public float knockbackPower;
}