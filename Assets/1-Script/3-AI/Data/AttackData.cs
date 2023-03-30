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
    public GameObject attackVFX;
    public float attackSpeed;
    public float waitTime;
    public float attackInRange;
    public int damage;
    public float scaleBegin;
    public float scaleEnd;
    public float time;
}