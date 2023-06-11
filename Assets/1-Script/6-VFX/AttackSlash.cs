using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlash : MonoBehaviour
{
    DamageMeleeData damageMeleeData;
    public float damage;

    LinkedList<GameObject> hitedGOs;

    private void Awake()
    {
        hitedGOs = new LinkedList<GameObject>();
    }

    public void SetDamageMeleeData(DamageMeleeData damageMeleeData)
    {
        this.damageMeleeData = damageMeleeData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (hitedGOs.Contains(other.gameObject)) return;
            other.GetComponentInParent<Unit>().DamageMelee(damage, damageMeleeData);
            hitedGOs.AddLast(other.gameObject);
        }
    }

    private void OnDisable()
    {
        hitedGOs.Clear();
    }
}