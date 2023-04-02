using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackData
{
    public enum AttackType
    {
        Ranged,
        Melee
    }

    public AttackType attackType;

    [Header("Generic Settings")]
    public GameObject attackVFX;
    public float waitTime;
    public float attackInRange;
    public int damage;

    [Header("Ranged Settings")]
    public float attackSpeed;

    [Header("Melee Settings")]
    public float scaleAttackBegin;
    public float time;
}