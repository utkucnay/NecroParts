using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlash : MonoBehaviour
{
    public int damage;
    Vector3 pos;

    LinkedList<GameObject> hitedGOs;

    private void Awake()
    {
        hitedGOs = new LinkedList<GameObject>();
    }

    public void SetPos(in Vector3 pos)
    {
        this.pos = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (hitedGOs.Contains(other.gameObject)) return;
            other.GetComponentInParent<Unit>().DamageMelee(damage, new DamageMeleeData() { meleePos = pos });
            hitedGOs.AddLast(other.gameObject);
        }
    }

    private void OnDisable()
    {
        hitedGOs.Clear();
    }
}