using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    LinkedList<Weapon> weapons;

    [SerializeField] GameObject thunderWeaponObj;

    private void Awake()
    {
        weapons = new();
        AddWeapon();
    }

    private void Update()
    {
        foreach (var weapon in weapons)
        {
            weapon.Update();
        }
    }

    public void AddWeapon()
    {
        weapons.AddLast(new ThunderWeapon(2, 10, thunderWeaponObj));
    }
}
 
public abstract class Weapon
{
    public float attackSpeed;
    public float damage;
    public GameObject prefab;
    public int count;

    float timer;

    public Weapon(float attackSpeed, float damage, GameObject prefab)
    {
        timer = 0;

        count = 1;
        this.attackSpeed = attackSpeed;
        this.damage = damage;
        if (prefab == null) Debug.LogWarning("Prefab Not Null");
        this.prefab = prefab;
    }

    public void Update()
    {
        if (timer > attackSpeed)
        {
            Debug.Log("Weapon");
            WeaponAttack();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    protected abstract void WeaponAttack();
}

class ThunderWeapon : Weapon
{
    public ThunderWeapon(float attackSpeed, float damage, GameObject prefab) : base(attackSpeed, damage, prefab)
    {
    }

    protected override void WeaponAttack()
    {
        var enemies = AIManager.s_Instance.GetCloseEnemies(12, Player.s_Instance.transform.position);
        if (enemies.Length == 0) return;
        var enemyCount = enemies.Length;
        var randMax = enemyCount / count;

        for (int i = 0; i < count; i++)
        {
            var rand = Random.Range(0, randMax);
            var enemy = enemies.ElementAt(rand);
            var spawnGO = GameObject.Instantiate(prefab);
            spawnGO.transform.position = enemy.transform.position;
            spawnGO.gameObject.SetActive(true);
            enemy.DamageMelee(damage, new DamageMeleeData());
        }
    }
}